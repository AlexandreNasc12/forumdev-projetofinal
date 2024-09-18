using System;
using FDV.Core.Messages;
using FluentValidation;

namespace FDV.Forum.App.Commands;

public class AdicionarCategoriaCommand : Command
{
    public string Nome { get; private set; }

    public string Descricao { get; private set; }

    public AdicionarCategoriaCommand(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public override bool EstaValido()
    {
        ValidationResult = new AdicionarCategoriaValidation().Validate(this);

        return ValidationResult.IsValid;
    }

    public class AdicionarCategoriaValidation : AbstractValidator<AdicionarCategoriaCommand>
    {
        public AdicionarCategoriaValidation()
        {
            RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("Informe um nome para categoria!")
            .NotNull().WithMessage("O nome da categoria não deve ser nulo!");

            RuleFor(c => c.Descricao)
            .NotNull().WithMessage("Informe uma descrição para a categoria!")
            .NotEmpty().WithMessage("Informe uma descrição para a categoria!");
        }
    }
}
