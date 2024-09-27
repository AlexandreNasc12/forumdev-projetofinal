using System;
using System.ComponentModel.DataAnnotations;

namespace FDV.WebAPI.InputModels;

public class PostagemInputModel
{
    [Required(ErrorMessage = "Informe um id de usuário!")]
    public Guid UsuarioId { get; set; }

    [Required(ErrorMessage ="Informe um titulo para o postagem")]
    public string Titulo { get; set; }

    [Required(ErrorMessage ="Informe uma descrição para a postagem")]
    public string Descricao { get; set; }

    public Guid[] CategoriasId {get; set;}
}

public class ModeracaoInputModel
{
    public bool Publicado { get; set; }
    public bool Aprovado { get; set; }
}