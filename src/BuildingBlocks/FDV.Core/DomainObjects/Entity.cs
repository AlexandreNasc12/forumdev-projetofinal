using System.Globalization;

namespace FDV.Core.DomainObjects;

public abstract class Entity
{
    public Guid Id { get; set; }

    public DateTime DataDeCadastro { get; set; }

    public DateTime DataDeAlteracao { get; set; }

    public string ObterDataFormatada() => DataDeCadastro.ToString("G",new CultureInfo("pt-Br"));

    public void AtribuirEntidadeId(Guid id) => Id = id;
}