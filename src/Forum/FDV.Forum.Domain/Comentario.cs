namespace FDV.Forum.Domain;

public class Comentario
{
    public Usuario Usuario { get; private set; }

    public string Descricao { get; private set; }

    public DateTime DataDeCadastro { get; private set; }

    protected Comentario(){}

    public Comentario(Usuario usuario, string descricao)
    {
        Usuario = usuario;
        Descricao = descricao;

        DataDeCadastro = DateTime.UtcNow;
    }
}
