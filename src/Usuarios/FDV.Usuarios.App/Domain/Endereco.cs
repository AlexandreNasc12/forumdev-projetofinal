using FDV.Core.ValueObjects;

namespace FDV.Usuarios.App.Domain;

public class Endereco
{
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public Cep Cep { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }

    protected Endereco(){}

    public Endereco(string logradouro, string numero, string complemento, 
    Cep cep, string bairro, string cidade, string estado)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    public override string ToString()
    {
        return $"{Logradouro}, {Complemento}, {Numero}, {Cep.NumeroFormatado}, {Bairro}, {Cidade} - {Estado}";
    }

    protected bool Equals(Endereco other)
    {
        return Logradouro.Trim().ToUpper() == other.Logradouro.Trim().ToUpper()
              && Complemento.Trim().ToUpper() == other.Complemento.Trim().ToUpper()
              && Numero == other.Numero
              && Cep.Numero == other.Cep.Numero
              && Bairro.Trim().ToUpper() == other.Bairro.Trim().ToUpper()
              && Cidade.Trim().ToUpper() == other.Cidade.Trim().ToUpper()
              && Estado.Trim().ToUpper() == other.Estado.Trim().ToUpper();
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;

        return Equals((Endereco)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Logradouro, Complemento, Numero, Cep.Numero, Bairro, Cidade, Estado);
    }
}