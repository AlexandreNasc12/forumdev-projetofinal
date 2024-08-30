namespace FDV.Forum.Domain;

public class Categoria
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }

    //EF
    public List<Postagem> _Postagens;
    public IReadOnlyCollection<Postagem> Postagems => _Postagens;

    protected Categoria(){}

    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }

    public void AtribuirNome(string nome) => Nome = nome;

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
}
