using Microsoft.AspNetCore.Mvc.Filters;

using TechBox.Api.Filters;

namespace TechBox.Api.Configurations;

public static class Filters
{
    public static void AddFilterConfiguration(this FilterCollection filterCollection)
    {
        filterCollection.Add(new ModelStateFilter());
        filterCollection.Add(new ExceptionFilter());
    }
}