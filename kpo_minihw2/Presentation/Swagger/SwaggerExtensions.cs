using Microsoft.AspNetCore.Builder;

namespace kpo_minihw2.Presentation.Swagger;

public static class SwaggerExtensions
{
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooApp API v1");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}