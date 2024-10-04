using System;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace FDV.WebAPI.Configuration;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Projeto Forum Estartando Devs 2024",
                Description = "Este projeto é um modelo para estudo dos alunos de Back-End",
                Contact = new OpenApiContact() { Name = "Alexandre Nascimento", Email = "alexandre@techdog.com.br" }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentname}/swagger.json";
        });

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum Estartando Devs 2024");
            c.RoutePrefix = "swagger";
        });
    }
}
