namespace TechBox.Api.Services;

public class ServiceResult
{
    public bool IsSuccess { get; private set; }
    public IList<ServiceResultError> Errors { get; private set; }

    public ServiceResult()
    {
        IsSuccess = true;
        Errors = new List<ServiceResultError>();
    }

    public void AddError(Tuple<string, string> typeMessage)
    {
        Errors.Add(new ServiceResultError(typeMessage.Item1, typeMessage.Item2));
        IsSuccess = false;
    }
}
