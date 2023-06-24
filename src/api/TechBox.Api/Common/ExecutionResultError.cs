namespace TechBox.Api.Common;

public sealed class ExecutionResultError
{
    public string Type { get; private set; } = ExecutionErrors.Unknown.Item1;
    public string Message { get; private set; } = ExecutionErrors.Unknown.Item2;

    public ExecutionResultError()
    {
    }

    public ExecutionResultError(string type, string message)
    {
        Type = type;
        Message = message;
    }
}
