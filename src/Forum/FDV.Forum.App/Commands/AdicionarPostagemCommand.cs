using System;
using FDV.Core.Messages;
using FDV.Forum.Domain;

namespace FDV.Forum.App.Commands;

public class AdicionarPostagemCommand : Command
{

    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Foto { get; private set; }

    public string Titulo { get; private set; }

    public string Descricao { get; private set; }

    public Guid[] CategoriaIds { get; private set; }

    public AdicionarPostagemCommand(Guid id, string nome, string foto, string titulo, 
    string descricao, Guid[] categoriaIds)
    {
        Id = id;
        Nome = nome;
        Foto = foto;
        Titulo = titulo;
        Descricao = descricao;
        CategoriaIds = categoriaIds;
    }
}