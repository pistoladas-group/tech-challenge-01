using TechBox.Api.Services;

namespace TechBox.Api.Configurations;

public static class FileStorage
{
    public static IServiceCollection AddFileStorageConfiguration(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IFileStorageService, AzureFileStorageService>();

        return services;
    }
}
