using System;
using FDV.Core.Messages;

namespace FDV.Usuarios.App.Application.Commands;

public class AtualizarUsuarioCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public string Foto { get; private set; }

    public AtualizarUsuarioCommand(Guid usuarioId, string nome, string cpf, DateTime dataDeNascimento, string foto)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Cpf = cpf;
        DataDeNascimento = dataDeNascimento;
        Foto = foto;
    }
}
