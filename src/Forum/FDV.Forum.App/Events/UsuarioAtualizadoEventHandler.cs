using System;
using FDV.Core.Messages;
using FDV.Forum.Domain;
using FDV.Forum.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FDV.Forum.App.Events;

public class UsuarioAtualizadoEventHandler : INotificationHandler<UsuarioAtualizadoEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public UsuarioAtualizadoEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task Handle(UsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var postagemRepository = scope.ServiceProvider.GetRequiredService<IPostagemRepository>();

            var usuario = new Usuario(notification.UsuarioId, notification.Nome, notification.Foto);
            
            postagemRepository.Atualizar(usuario);

            await Task.CompletedTask;
        }
    }
}
