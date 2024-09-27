using FDV.Core.Messages;

namespace FDV.Forum.App.Commands;

public class ComentarCommand : Command
{
    public Guid PostagemId { get; private set; }

    public Guid UsuarioId { get; private set; }

    public string Nome { get; private set; }

    public string Foto { get; private set; }

    public string Descricao { get; private set; }

    public ComentarCommand(Guid postagemId, Guid usuarioId, string nome, string foto, string descricao)
    {
        PostagemId = postagemId;
        UsuarioId = usuarioId;
        Nome = nome;
        Foto = foto;
        Descricao = descricao;
    }
}