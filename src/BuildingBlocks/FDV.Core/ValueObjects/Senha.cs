using FDV.Core.DomainObjects;

namespace FDV.Core.ValueObjects;

public class Senha : Aggregate
{
    public string Valor { get; private set; }

    protected Senha() { }

    public Senha(string valor)
    {
       Valor = ObterHash(valor);
    }
}
