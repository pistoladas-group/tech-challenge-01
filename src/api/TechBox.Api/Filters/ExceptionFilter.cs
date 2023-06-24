using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechBox.Api.Models;

namespace TechBox.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogCritical(context.Exception, context.Exception.Message);

        if (context.Result != null)
        {
            return;
        }

        context.Result = new ObjectResult(new ApiResponse(error: "There was an unexpected error with the application. Please contact support!"))
        {
            StatusCode = 500
        };
    }
}