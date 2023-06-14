using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using TechBox.Api.Models;

namespace TechBox.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
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