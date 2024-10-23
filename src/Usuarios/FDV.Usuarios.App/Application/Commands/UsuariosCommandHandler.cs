using FDV.Core.Messages;
using FDV.Core.ValueObjects;
using FDV.Usuarios.App.Domain;
using FDV.Usuarios.App.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace FDV.Usuarios.App.Application.Commands;

public class UsuariosCommandHandler : CommandHandler,
        IRequestHandler<AdicionarUsuarioCommand, ValidationResult>,
        IRequestHandler<AdicionarEnderecoCommand,ValidationResult>,
        IRequestHandler<AtualizarUsuarioCommand, ValidationResult>, IDisposable
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


    public async Task<ValidationResult> Handle(AdicionarEnderecoCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario is null)
        {
            AdicionarErro("Usuário não encontrado!");
            return ValidationResult;
        }

        var endereco = new Endereco(request.Logradouro,request.Numero,request.Complemento, 
        new Cep(request.Cep),request.Bairro, request.Cidade, request.Estado);

        usuario.Adicionar(endereco);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario is null)
        {
            AdicionarErro("Usuário não encontrado!");
            return ValidationResult;
        }

        usuario.AtribuirNome(request.Nome);
        usuario.AtribuirFoto(request.Foto);
        usuario.AtribuirDataDeNascimento(request.DataDeNascimento);

        _usuarioRepository.Atualizar(usuario);

        var evento = new UsuarioAtualizadoEvent(usuario.Id,usuario.Nome,usuario.Foto);

        usuario.AdicionarEvento(evento);
    
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public void Dispose()
    {
        _usuarioRepository.Dispose();
    }

}
