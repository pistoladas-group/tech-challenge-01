namespace TechBox.Api.Models;

public class ApiResponse
{
    public bool Succeeded { get; private set; }
    public object? Data { get; init; }
    public List<string>? Errors { get; init; }

    public ApiResponse()
    {
        Succeeded = true;
    }

    public ApiResponse(object? data)
    {
        Succeeded = true;
        Data = data;
    }

    public ApiResponse(string error)
    {
        Succeeded = false;
        Errors ??= new List<string>();
        Errors.Add(error);
    }

    public ApiResponse(List<string> errors)
    {
        Succeeded = false;
        Errors = errors;
    }
}
