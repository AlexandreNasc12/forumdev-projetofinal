using System;
using MediatR;

namespace FDV.Core.Messages;

public abstract class Event : Message, INotification
{
    public DateTime Timestamp { get; private set;}

    public Event()
    {
        Timestamp = DateTime.UtcNow;
    }
}
