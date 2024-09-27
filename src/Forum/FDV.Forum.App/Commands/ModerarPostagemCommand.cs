using System;
using FDV.Core.Messages;

namespace FDV.Forum.App.Commands;

public class ModerarPostagemCommand : Command
{
    public Guid PostagemId { get; private set; }
    public bool Publicado { get; private set; }
    public bool Aprovado { get; private set; }

    public ModerarPostagemCommand(bool publicado, bool aprovado, Guid postagemId)
    {
        Publicado = publicado;
        Aprovado = aprovado;
        PostagemId = postagemId;
    }

    public bool EstaPublicadoNaoAprovado() => !Aprovado && Publicado;
}
