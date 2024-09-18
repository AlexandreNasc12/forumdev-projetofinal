using FDV.Core.Messages;
using FDV.Core.ValueObjects;
using FDV.Usuarios.App.Domain;
using FDV.Usuarios.App.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace FDV.Usuarios.App.Application.Commands;

public class UsuariosCommandHandler : CommandHandler,
        IRequestHandler<AdicionarUsuarioCommand, ValidationResult>, IDisposable
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ValidationResult> Handle(AdicionarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var cpf = new Cpf(request.Cpf);

        if (!cpf.EstaValido())
        {
            AdicionarErro("Cpf inválido!");
            return ValidationResult;
        }

        var novo = new Usuario(request.Nome,cpf,request.Foto,request.DataDeNascimento);

        var login = new Login(new Email(request.Email),new Senha(request.Senha));
        
        novo.AtribuirLogin(login);

        _usuarioRepository.Adicionar(novo);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public void Dispose()
    {
        _usuarioRepository.Dispose();
    }
}
