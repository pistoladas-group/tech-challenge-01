using Serilog;
using TechBox.Api.Data;
using TechBox.Api.Models;

namespace TechBox.Api.Services;

public class FileBackgroundService : IHostedService, IDisposable
{
    private readonly IRemoteFileStorageService _remoteFileStorageService;
    private readonly ILocalFileStorageService _localFileStorageService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IFileRepository _fileRepository;
    private Timer? _timer;
    private bool _isProcessing;

    public FileBackgroundService(IServiceScopeFactory serviceScopeFactory, IRemoteFileStorageService remoteFileStorageService, ILocalFileStorageService localFileStorageService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _remoteFileStorageService = remoteFileStorageService;
        _localFileStorageService = localFileStorageService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(ExecuteProcessAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    private async void ExecuteProcessAsync(object? state)
    {
        if (_isProcessing)
        {
            Log.Debug("Background service last execution still running. Aborting.");
            return;
        }

        _isProcessing = true;
        Log.Information("Background service started.");

        using var scope = _serviceScopeFactory.CreateScope();
        _fileRepository = scope.ServiceProvider.GetRequiredService<IFileRepository>();

        var pendingFileIds = await _fileRepository.ListPendingFileIdsAsync(1, int.MaxValue);
        var convertedFileIds = pendingFileIds.ToList();

        Log.Information("Total of files logs to process: {Count}.", convertedFileIds.Count);

        for (int index = 0; index < convertedFileIds.Count; index++)
        {
            Guid pendingFileId = convertedFileIds[index];
            Log.Information("Working on file {FileId}. Number {Number} of {Total}.", pendingFileId, index + 1, convertedFileIds.Count);

            var pendingFileLogs = await _fileRepository.ListFilePendingLogsAsync(pendingFileId, 1, int.MaxValue);
            var convertedPendingLogs = pendingFileLogs.ToList();

            await _fileRepository.UpdateFileLogToProcessingByFileIdAsync(pendingFileId);
            Log.Debug("File {FileId} processing marked as {Processing}.", pendingFileId, "Processing");

            var hasPendingDelete = convertedPendingLogs.Any(x => x.ProcessTypeId == ProcessTypesEnum.Delete);
            var hasPendingUpload = convertedPendingLogs.Any(x => x.ProcessTypeId == ProcessTypesEnum.Upload);

            if (!hasPendingUpload && !hasPendingDelete)
            {
                Log.Warning("No log found for the pending file {FileId}. Skipping it.", pendingFileId);
                continue;
            }

            if (hasPendingUpload && hasPendingDelete)
            {
                Log.Information("File {FileId} has both upload and delete logs.", pendingFileId);

                _localFileStorageService.DeleteFile(pendingFileId, convertedPendingLogs.First().FileName);
                Log.Debug("File {FileId} was deleted from the disk.", pendingFileId);

                await _fileRepository.UpdateFileLogToSuccessByFileIdAndProcessTypeAsync(pendingFileId, ProcessTypesEnum.Upload);
                await _fileRepository.UpdateFileLogToSuccessByFileIdAndProcessTypeAsync(pendingFileId, ProcessTypesEnum.Delete);
                Log.Debug("File {FileId} processing marked as {Success}.", pendingFileId, "Success");

                continue;
            }

            if (hasPendingUpload)
            {
                try
                {
                    await UploadFileAsync(pendingFileId, convertedPendingLogs.First().FileName, convertedPendingLogs.First().FileContentType);
                }
                catch (Exception e)
                {
                    await HandleExecutionError(e, pendingFileId, ProcessTypesEnum.Upload);
                }
            }

            if (hasPendingDelete)
            {
                try
                {
                    await DeleteFileAsync(pendingFileId, convertedPendingLogs.First().FileName);
                }
                catch (Exception e)
                {
                    await HandleExecutionError(e, pendingFileId, ProcessTypesEnum.Delete);
                }
            }

            Log.Information("Processing on file {FileId} has finished.", pendingFileId);
        }

        _isProcessing = false;
        Log.Information("Background service finished.");
    }

    private async Task HandleExecutionError(Exception e, Guid pendingFileId, ProcessTypesEnum processTypesId)
    {
        Log.Error(e, "Error while processing {ProcessType} for the file {File}.", pendingFileId, processTypesId == ProcessTypesEnum.Upload ? "upload" : "delete");
        await _fileRepository.UpdateFileLogToFailedByIdAsync(pendingFileId, e.Message);
        await _fileRepository.UpdateFileProcessStatusByIdAsync(pendingFileId, ProcessStatusEnum.Failed);
    }

    private async Task DeleteFileAsync(Guid fileId, string fileName)
    {
        Log.Information("Deleting file {FileId}.", fileId);

        await _remoteFileStorageService.DeleteFileAsync(fileName);
        Log.Debug("file {FileId} was deleted from remote storage", fileId);

        await _fileRepository.UpdateFileLogToSuccessByFileIdAndProcessTypeAsync(fileId, ProcessTypesEnum.Delete);
        await _fileRepository.UpdateFileByIdAsync(fileId, null, true);
        Log.Debug("File {FileId} processing marked as {Success}.", fileId, "Success");
    }

    private async Task UploadFileAsync(Guid fileId, string fileName, string contentType)
    {
        Log.Information("Uploading file {FileId}.", fileId);

        var file = _localFileStorageService.GetFileById(fileId, fileName);
        var uploadedFileUri = await _remoteFileStorageService.UploadFileAsync(file, fileName, contentType);

        Log.Debug("file {FileId} was uploaded to the remote storage. File Url {Url}", fileId, uploadedFileUri);

        _localFileStorageService.DeleteFile(fileId, fileName);
        Log.Debug("File {FileId} was deleted from the disk.", fileId);

        await _fileRepository.UpdateFileLogToSuccessByFileIdAndProcessTypeAsync(fileId, ProcessTypesEnum.Upload);
        await _fileRepository.UpdateFileByIdAsync(fileId, uploadedFileUri, false);
        Log.Debug("File {FileId} processing marked as {Success}.", fileId, "Success");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Background service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}