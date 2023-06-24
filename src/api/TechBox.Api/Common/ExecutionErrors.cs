namespace TechBox.Api.Common;

public static class ExecutionErrors
{
    // Type, Message
    public static readonly Tuple<string, string> Unknown = new(nameof(Unknown), "Unknown error occurred");
    public static readonly Tuple<string, string> InvalidFileName = new(nameof(InvalidFileName), "The file name is invalid");
    public static readonly Tuple<string, string> UnsupportedExtension = new(nameof(UnsupportedExtension), "The extension of the file is not supported");
    public static readonly Tuple<string, string> UnsupportedFileSize = new(nameof(UnsupportedFileSize), "The size of the file is too large");
    public static readonly Tuple<string, string> DatabaseExecution = new(nameof(DatabaseExecution), "An error occurred while executing in database");
}
