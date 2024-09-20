using System;
using FDV.Core.DomainObjects;

namespace FDV.Forum.Domain;

public class Postagem : Entity, IAggregateRoot
{
    public const int TamanhoMaximo = 500;

    public Usuario Usuario { get; private set; }

    public string Titulo { get; private set; }

    public string Descricao { get; private set; }

    public bool Publicado { get; private set; }

    public bool Aprovado { get; private set; }

    public string AprovadoPor { get; private set; }

    public int QuantidadeComentario { get; private set; }

    //EF

    public List<Categoria> _Categorias;
    public IReadOnlyCollection<Categoria> Categorias => _Categorias;

    private List<Comentario> _Comentarios;
    public IReadOnlyCollection<Comentario> Comentarios => _Comentarios;

    protected Postagem()
    {
        _Categorias = new List<Categoria>();
        _Comentarios = new List<Comentario>();
    }

    public Postagem(Usuario usuario, string titulo, string descricao) : this()
    {
        Usuario = usuario;
        Titulo = titulo;
        Descricao = descricao;
    }

    public void Adicionar(Comentario comentario)
    {
        _Comentarios.Add(comentario);

        QuantidadeComentario++;
    }

    public void Remover(Comentario comentario)
    {
        _Comentarios.Remove(comentario);

        QuantidadeComentario--;
    }

    public void Adicionar(Categoria categoria)
    {
        _Categorias.Add(categoria);
    }

    public void Remover(Categoria categoria)
    {
        _Categorias.Remove(categoria);
    }

    public void Publicar() => Publicado = true;

    public void Ocultar() => Publicado = false;

    public void Aprovar() => Aprovado = true;

    public void Reprovar() => Aprovado = false;

}
