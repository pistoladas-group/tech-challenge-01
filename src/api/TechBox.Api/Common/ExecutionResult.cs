namespace TechBox.Api.Common;

public class ExecutionResult
{
    public bool IsSuccess { get; private set; }
    public IList<ExecutionResultError> Errors { get; private set; }

    public ExecutionResult()
    {
        IsSuccess = true;
        Errors = new List<ExecutionResultError>();
    }

    public void AddError(Tuple<string, string> typeMessage)
    {
        Errors.Add(new ExecutionResultError(typeMessage.Item1, typeMessage.Item2));
        IsSuccess = false;
    }
}
