using System.Text;

using dotenv.net;

namespace TechBox.Api.Configurations;

public static class EnvironmentVariables
{
    public static string DatabaseConnectionString => "TECHBOX_API_DATABASE_CONNECTION_STRING";

    public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services)
    {
        DotEnv.Fluent()
            .WithEnvFiles()
            .WithTrimValues()
            .WithEncoding(Encoding.UTF8)
            .WithOverwriteExistingVars()
            .WithProbeForEnv(probeLevelsToSearch: 6)
            .Load();

        return services;
    }
}