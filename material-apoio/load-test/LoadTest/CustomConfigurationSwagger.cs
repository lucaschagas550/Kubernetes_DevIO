using Microsoft.OpenApi.Models;
namespace Microsoft.Extensions.Configuration;

public static class CustomConfigurationSwagger
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Deployment - Demo",
                Contact = new OpenApiContact()
                {
                    Email = "bhdebrito@gmail.com",
                    Name = "Bruno Brito",
                    Url = new Uri("https://brunobrito.net.br")
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();
        return app;
    }
}