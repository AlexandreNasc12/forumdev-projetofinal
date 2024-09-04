using System;
using FDV.Core.Data;
using FDV.Core.DomainObjects;
using FDV.Core.Mediator;
using FDV.Core.Messages;
using FDV.Core.Ultilities;
using FDV.Usuarios.App.Domain;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace FDV.Usuarios.App.Infra.Data;

public class UsuarioContext : DbContext, IUnitOfWorks
{
    private readonly IMediatorHandler _mediatorHandler;

    public DbSet<Usuario> Usuarios { get; set; }

    public UsuarioContext(DbContextOptions<UsuarioContext> options, IMediatorHandler mediatorHandler)
    : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);
    }


    public async Task<bool> Commit()
    {
        var cetZone = ZonaDeTempo.ObterZonaDeTempo();

        foreach (var entry in ChangeTracker.Entries()
                    .Where(entry => entry.Entity.GetType().GetProperty("DataDeCadastro") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataDeCadastro").CurrentValue = 
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,cetZone);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataDeCadastro").IsModified = false;
                entry.Property("DataDeAlteracao").CurrentValue = 
                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,cetZone);
            }  
        }

        var sucesso = await SaveChangesAsync() > 0;

        if (sucesso) await _mediatorHandler.PublicarEventos(this);

        return sucesso;
    }
}


public static class MediatorExtension
{
    public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notificacoes)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.LimparEventos());

        var tasks = domainEvents.Select(async (domainEvent) => {await mediator.PublicarEvento(domainEvent);});

        await Task.WhenAll(tasks);
    }
}
