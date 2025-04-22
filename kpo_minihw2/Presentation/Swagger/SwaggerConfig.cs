using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace kpo_minihw2.Presentation.Swagger;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ZooApp API",
                Version = "v1",
                Description = "API for managing animals, enclosures, and feeding schedules in the zoo.",
                Contact = new OpenApiContact
                {
                    Name = "Egor Gruzintsev",
                    Email = "gruz2520@gmail.com"
                }
            });

            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (System.IO.File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }
}