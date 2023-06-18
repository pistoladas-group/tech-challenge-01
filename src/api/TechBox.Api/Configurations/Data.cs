using TechBox.Api.Data;
using TechBox.Api.Data.SqlServer;

namespace TechBox.Api.Configurations
{
    public static class Data
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IFileRepository, FileRepository>();

            // Sql Server
            services.AddScoped<IStoredProcedureHandler, SqlServerStoredProcedureHandler>();
            services.AddScoped(_ => new SqlServerSession(Environment.GetEnvironmentVariable(EnvironmentVariables.DatabaseConnectionString) ?? throw new ArgumentNullException()));

            return services;
        }
    }
}
