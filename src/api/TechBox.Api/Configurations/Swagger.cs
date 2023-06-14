using System.Reflection;

using Microsoft.OpenApi.Models;

namespace TechBox.Api.Configurations;

public static class Swagger
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TechBox API",
                Description = "The TechBox project API",
                TermsOfService = new Uri("https://github.com/pistoladas-group/tech-challenge-01"),
                Contact = new OpenApiContact
                {
                    Name = "Github Issues",
                    Url = new Uri("https://github.com/pistoladas-group/tech-challenge-01/issues"),
                    Email = "pistoladas-group@gmail.com"
                }
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}