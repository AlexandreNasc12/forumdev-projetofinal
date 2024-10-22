using System;
using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.Forum.App.Queries;
using FDV.Forum.Domain.Interfaces;
using FDV.Forum.Infra.Repositories;
using FDV.Usuarios.App.Application.Commands;
using FDV.Usuarios.App.Application.Queries;
using FDV.Usuarios.App.Domain.Interfaces;
using FDV.Usuarios.App.Infra.Repositories;
using FluentValidation.Results;
using MediatR;

namespace FDV.WebAPI.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IPostagemRepository, PostagemRepository>();
        services.AddScoped<IUsuarioQueries, UsuarioQueries>();
        services.AddScoped<IPostagensQueries, PostagensQueries>();

        services.AddScoped<ICategoriasQueries, CategoriasQueries>();
        //Contexto de Usuários
        services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, ValidationResult>, UsuariosCommandHandler>();
        services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, UsuariosCommandHandler>();

        //Contexto de Postagens
        services.AddScoped<IRequestHandler<AdicionarCategoriaCommand, ValidationResult>, PostagensCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarCategoriaCommand, ValidationResult>, PostagensCommandHandler>();
        services.AddScoped<IRequestHandler<AdicionarPostagemCommand, ValidationResult>, PostagensCommandHandler>();
        services.AddScoped<IRequestHandler<ModerarPostagemCommand, ValidationResult>, PostagensCommandHandler>();
        services.AddScoped<IRequestHandler<ComentarCommand, ValidationResult>, PostagensCommandHandler>();
    }
}
