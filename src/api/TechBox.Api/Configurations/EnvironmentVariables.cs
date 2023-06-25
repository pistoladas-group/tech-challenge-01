using System.Text;

using dotenv.net;

namespace TechBox.Api.Configurations;

public static class EnvironmentVariables
{
    public static string DatabaseConnectionString => "TECHBOX_API_DATABASE_CONNECTION_STRING";
    public static string StorageAccountUrl => "TECHBOX_API_AZURE_STORAGE_ACCOUNT_URL";
    public static string StorageAccountContainerName => "TECHBOX_API_AZURE_STORAGE_ACCOUNT_CONTAINER_NAME";
    public static string DiscordWebhookId => "TECHBOX_API_DISCORD_WEBHOOK_ID";
    public static string DiscordWebhookToken => "TECHBOX_API_DISCORD_WEBHOOK_TOKEN";

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