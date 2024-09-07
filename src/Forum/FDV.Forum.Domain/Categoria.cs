using FDV.Core.DomainObjects;

namespace FDV.Forum.Domain;

public class Categoria : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }

    //EF
    public List<Postagem> _Postagens;
    public IReadOnlyCollection<Postagem> Postagens => _Postagens;

    protected Categoria(){}

    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public void AtribuirNome(string nome) => Nome = nome;

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
}
