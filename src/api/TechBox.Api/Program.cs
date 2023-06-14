using TechBox.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
