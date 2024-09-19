using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.Forum.App.Queries;
using FDV.Forum.Domain.Interfaces;
using FDV.Forum.Infra.Data;
using FDV.Forum.Infra.Repositories;
using FDV.Usuarios.App.Application.Commands;
using FDV.Usuarios.App.Application.Queries;
using FDV.Usuarios.App.Domain.Interfaces;
using FDV.Usuarios.App.Infra.Data;
using FDV.Usuarios.App.Infra.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<UsuarioContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("ForumConnection")));

builder.Services.AddDbContext<PostagensContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("ForumConnection")));

builder.Services.AddScoped<IMediatorHandler,MediatorHandler>();
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<IPostagemRepository,PostagemRepository>();
builder.Services.AddScoped<IUsuarioQueries,UsuarioQueries>();

builder.Services.AddScoped<ICategoriasQueries,CategoriasQueries>();

//Contexto de Usuários
builder.Services.AddScoped<IRequestHandler<AdicionarUsuarioCommand,ValidationResult>,UsuariosCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AdicionarEnderecoCommand,ValidationResult>,UsuariosCommandHandler>();

//Contexto de Postagens
builder.Services.AddScoped<IRequestHandler<AdicionarCategoriaCommand,ValidationResult>, PostagensCommandHandler>();
builder.Services.AddScoped<IRequestHandler<AtualizarCategoriaCommand,ValidationResult>, PostagensCommandHandler>();


builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();