using System;
using System.Net;
using FluentValidation.Results;
using MediatR;

namespace FDV.Core.Messages;

public abstract class Command : Message, IRequest<ValidationResult> 
{
    public DateTime Timestamp { get; private set; }

    public ValidationResult ValidationResult { get; set; }

    public Command()
    {
        Timestamp = DateTime.UtcNow;
        ValidationResult = new ValidationResult();
    }

    public virtual bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}
