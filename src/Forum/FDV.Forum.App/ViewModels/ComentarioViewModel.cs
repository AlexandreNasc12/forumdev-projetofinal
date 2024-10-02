using System.Globalization;
using FDV.Forum.Domain;

namespace FDV.Forum.App.ViewModels;

public class ComentarioViewModel
{

    public Guid UsuarioId { get; set; }

    public string Nome { get; set; }

    public string Foto { get; set; }

    public string Descricao { get; set; }

    public string DataDeCadastro { get; set; }

    public static ComentarioViewModel Mapear(Comentario comentario)
    {
        return new ComentarioViewModel()
        {
            UsuarioId = comentario.Usuario.Id,
            Nome = comentario.Usuario.Nome,
            Foto = comentario.Usuario.Foto,
            Descricao = comentario.Descricao,
            DataDeCadastro = comentario.DataDeCadastro.ToString("G",new CultureInfo("pt-Br"))
        };
    }
}
