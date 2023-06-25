using TechBox.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers(options => options.Filters.AddFilterConfiguration());

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration()
    .AddEnvironmentVariables()
    .AddLoggingConfiguration(builder.Host)
    .AddDataConfiguration()
    .AddInfrastructureConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
