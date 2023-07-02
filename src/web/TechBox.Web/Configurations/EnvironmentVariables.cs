using System.Text;
using dotenv.net;

namespace TechBox.Web.Configurations;

public static class EnvironmentVariables
{
    public static string ApiBaseUrl => "TECHBOX_APP_API_MAIN_URL";

    public static IServiceCollection AddEnvironmentVariables(this IServiceCollection services)
    {
        try
        {
            DotEnv.Fluent()
                .WithEnvFiles()
                .WithTrimValues()
                .WithEncoding(Encoding.UTF8)
                .WithOverwriteExistingVars()
                .WithProbeForEnv(probeLevelsToSearch: 6)
                .Load();
        }
        catch (Exception)
        {
            // ignored. Is using production runtime environment variables
        }

        return services;
    }
}