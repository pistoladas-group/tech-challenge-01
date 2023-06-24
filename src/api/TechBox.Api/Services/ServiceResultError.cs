using TechBox.Api.Configurations;

namespace TechBox.Api.Services;

public sealed class ServiceResultError
{
    public string Type { get; private set; } = ApiErrors.Unknown.Item1;
    public string Message { get; private set; } = ApiErrors.Unknown.Item2;

    public ServiceResultError()
    {
    }

    public ServiceResultError(string type, string message)
    {
        Type = type;
        Message = message;
    }
}
