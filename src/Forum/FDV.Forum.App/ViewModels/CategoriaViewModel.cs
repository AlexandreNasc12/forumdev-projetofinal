using System;
using FDV.Forum.Domain;

namespace FDV.Forum.App.ViewModels;

public class CategoriaViewModel
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public string DataCadastro { get; set; }

    public static CategoriaViewModel Mapear(Categoria categoria)
    {
        return new CategoriaViewModel()
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            DataCadastro = categoria.ObterDataFormatada()
        };
    }
}
