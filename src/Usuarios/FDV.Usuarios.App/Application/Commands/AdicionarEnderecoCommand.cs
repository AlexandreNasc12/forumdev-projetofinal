using System;
using FDV.Core.Messages;

namespace FDV.Usuarios.App.Application.Commands;

public class AdicionarEnderecoCommand : Command
{
    public AdicionarEnderecoCommand(Guid usuarioId, string logradouro, string numero, 
    string complemento, string cep, string bairro, string cidade, string estado)
    {
        UsuarioId = usuarioId;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    public Guid UsuarioId { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public string Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
}
