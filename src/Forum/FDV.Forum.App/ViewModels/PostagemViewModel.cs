using System;
using FDV.Forum.Domain;

namespace FDV.Forum.App.ViewModels;

public class PostagemViewModel
{
    public Guid Id { get; set; }
    public string UsuarioNome { get; set; }

    public string Titulo { get; set; }

    public string Descricao { get; set; }

    public int QuantidadeComentario { get; set; }

    public IEnumerable<CategoriaViewModel> Categorias { get; set; }

    public static PostagemViewModel Mapear(Postagem postagem)
    {
        return new PostagemViewModel()
        {
            Id = postagem.Id,
            UsuarioNome = postagem.Usuario.Nome,
            Titulo = postagem.Titulo,
            Descricao = postagem.Descricao,
            QuantidadeComentario = postagem.QuantidadeComentario,
            Categorias = postagem.Categorias.Select(CategoriaViewModel.Mapear)
        };
    }
}
