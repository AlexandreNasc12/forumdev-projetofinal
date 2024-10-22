using System;
using FDV.Forum.Infra.Data;
using FDV.Usuarios.App.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FDV.WebAPI.Configuration;

public static class ApiConfig
{
    private const string ConexaoBancoDeDados = "ForumConnection";
    private const string PermissoesDeOrigem = "_premissoesDeOrigem";

    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddDbContext<UsuarioContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConexaoBancoDeDados)));

        services.AddDbContext<PostagensContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString(ConexaoBancoDeDados)));

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddCors(options =>
        {
            // options.AddPolicy(PermissoesDeOrigem,
            // policy =>{
            //     policy.WithOrigins("http://www.conectedu.com",
            //                        "http://conectedu.com");
            // });

            options.AddPolicy(PermissoesDeOrigem,
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.MapControllers();
        
        app.UseHttpsRedirection();
    }
}
