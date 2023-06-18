using TechBox.Api.Data;
using TechBox.Api.Data.SqlServer;

namespace TechBox.Api.Configurations
{
    public static class Data
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IFileRepository, FileRepository>();

            // Sql Server
            services.AddScoped<IStoredProcedureHandler, SqlServerStoredProcedureHandler>();
            services.AddScoped(service => new SqlServerSession(configuration.GetConnectionString("SqlServer") ?? throw new ArgumentNullException()));

            return services;
        }
    }
}
