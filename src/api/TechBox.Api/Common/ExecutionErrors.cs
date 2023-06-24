namespace TechBox.Api.Common;

public static class ExecutionErrors
{
    // Type, Message
    public static readonly Tuple<string, string> Unknown = new Tuple<string, string>(nameof(Unknown), "Unknown error ocurred");
    public static readonly Tuple<string, string> InvalidFileName = new Tuple<string, string>(nameof(InvalidFileName), "The file name is invalid");
    public static readonly Tuple<string, string> UnsupportedExtension = new Tuple<string, string>(nameof(UnsupportedExtension), "The extension of the file is not supported");
    public static readonly Tuple<string, string> UnsupportedFileSize = new Tuple<string, string>(nameof(UnsupportedFileSize), "The size of the file is too large");

    public static readonly Tuple<string, string> DatabaseExecution = new Tuple<string, string>(nameof(DatabaseExecution), "An error ocurred while executing in database");
}
