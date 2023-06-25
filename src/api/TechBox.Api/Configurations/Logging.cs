using Serilog;
using Serilog.Events;
using Serilog.Sinks.Discord;

namespace TechBox.Api.Configurations;

public static class Logging
{
    public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, ConfigureHostBuilder host)
    {
        var webhookId = Convert.ToUInt64(Environment.GetEnvironmentVariable(EnvironmentVariables.DiscordWebhookId));
        var webhookToken = Environment.GetEnvironmentVariable(EnvironmentVariables.DiscordWebhookToken);
        
        host.UseSerilog((_, lc) => lc
            .WriteTo.Console()
            .WriteTo.Discord(webhookId: webhookId, webhookToken: webhookToken, restrictedToMinimumLevel: LogEventLevel.Warning)
            .MinimumLevel.Verbose()
        );
        
        return services;
    }
}
