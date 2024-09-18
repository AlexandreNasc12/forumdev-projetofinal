using System;
using FDV.Core.Messages;

namespace FDV.Usuarios.App.Application.Commands;

public class AdicionarUsuarioCommand : Command
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public string Foto { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public AdicionarUsuarioCommand(string nome, string cpf,
    DateTime dataDeNascimento, string foto, string email, string senha)
    {
        Nome = nome;
        Cpf = cpf;
        DataDeNascimento = dataDeNascimento;
        Foto = foto;
        Email = email;
        Senha = senha;
    }
}
