using TechBox.Api.Services;

namespace TechBox.Api.Configurations;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IRemoteFileStorageService, AzureRemoteFileStorageService>();
        services.AddSingleton<ILocalFileStorageService, LocalFileStorageService>();
        services.AddHostedService<FileBackgroundService>();

        return services;
    }
}
