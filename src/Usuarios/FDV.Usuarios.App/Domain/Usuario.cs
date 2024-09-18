using System;
using FDV.Core.DomainObjects;
using FDV.Core.ValueObjects;

namespace FDV.Usuarios.App.Domain;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public Cpf Cpf { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public string Foto { get; private set; }
    public Login Login { get; private set; }

    private HashSet<Endereco> _Enderecos;
    public IReadOnlyCollection<Endereco> Enderecos => _Enderecos;

    public int Idade
    {
        get
        {
            if (DataDeNascimento == DateTime.MinValue) return 0;

            var idade = DateTime.Now.Year - DataDeNascimento.Year;

            if (DateTime.Now.Month < DataDeNascimento.Month ||
            (DateTime.Now.Month == DataDeNascimento.Month && DateTime.Now.Day < DataDeNascimento.Day))
            {
                idade--;
            }

            return idade;
        }
    }

    private Usuario()
    {
        _Enderecos = new HashSet<Endereco>();
    }

    public Usuario(string nome, Cpf cpf, string foto, DateTime dataDeNascimento) : this()
    {
        Nome = nome;
        Cpf = cpf;
        Foto = foto;
        DataDeNascimento = dataDeNascimento;
    }

    public void AtribuirNome(string nome) => Nome = nome;

    public void AtribuirLogin(Login login) => Login = login;

    public void AtribuirDataDeNascimento(DateTime dataNascimento) => DataDeNascimento = dataNascimento;

    public void AtribuirFoto(string foto) => Foto = foto;

    public void Adicionar(Endereco endereco) => _Enderecos.Add(endereco);

    public void Remover(Endereco endereco) => _Enderecos.Remove(endereco);
}