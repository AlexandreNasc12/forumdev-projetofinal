using System;
using FDV.Core.Messages;
using MediatR;

namespace FDV.Forum.App.Events;

public class UsuarioAtualizadoEventHandler : INotificationHandler<UsuarioAtualizadoEvent>
{
    public async Task Handle(UsuarioAtualizadoEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
