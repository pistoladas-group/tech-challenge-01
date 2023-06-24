namespace TechBox.Api.Common;

public class ProcedureExecutionException : Exception
{
    private const string _defaultMessage = "An error ocurred while executing the Procedure";

    public ProcedureExecutionException() : base(_defaultMessage)
    {
    }

    public ProcedureExecutionException(string? procedureName) : base($"{_defaultMessage} {procedureName}")
    {
    }

    public ProcedureExecutionException(Exception? innerException, string? message = _defaultMessage) : base(message, innerException)
    {
    }
}
