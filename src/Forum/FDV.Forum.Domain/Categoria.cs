using FDV.Core.DomainObjects;
using FDV.Core.Ultilities;

namespace FDV.Forum.Domain;

public class Categoria : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public Guid Hash { get; private set; }

    //EF
    public List<Postagem> _Postagens;
    public IReadOnlyCollection<Postagem> Postagens => _Postagens;

    protected Categoria() { }

    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        Hash = new Identidade(Nome.Trim().RemoveAcentos().ToLower());
    }

    public static Guid GerarHash(string nome) => new Identidade(nome.Trim().RemoveAcentos().ToLower());

    public void AtribuirNome(string nome)
    {
        Nome = nome;
        Hash = new Identidade(Nome.Trim().RemoveAcentos().ToLower());
    }

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
}
