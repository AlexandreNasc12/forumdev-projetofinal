using System;
using FDV.Core.Messages;
using FluentValidation;

namespace FDV.Forum.App.Commands;

public class AtualizarCategoriaCommand : Command
{
    public Guid CategoriaId { get; private set; }

    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public AtualizarCategoriaCommand(Guid categoriaId, string nome, string descricao)
    {
        CategoriaId = categoriaId;
        Nome = nome;
        Descricao = descricao;
    }

    public override bool EstaValido()
    {
        ValidationResult = new AtualizarCategoriaValidation().Validate(this);

        return ValidationResult.IsValid;
    }

    public class AtualizarCategoriaValidation : AbstractValidator<AtualizarCategoriaCommand>
    {
        public AtualizarCategoriaValidation()
        {
            RuleFor(c => c.CategoriaId)
                .NotEqual(Guid.Empty).WithMessage("Id da categoria é inválido!");

            RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Informe um nome para categoria!")
            .NotNull().WithMessage("O nome da categoria não deve ser nulo!");

            RuleFor(c => c.Descricao)
            .NotNull().WithMessage("Informe uma descrição para a categoria!")
            .NotEmpty().WithMessage("Informe uma descrição para a categoria!");
        }
    }
}
