using System;
using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.Usuarios.App.Application.Queries;
using FDV.WebApi.Core.Controllers;
using FDV.WebAPI.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FDV.WebAPI.Controllers;

[Route("api/postagem")]
public class PostagensController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    private readonly IUsuarioQueries _usuarioQueries;

    public PostagensController(IMediatorHandler mediatorHandler,
    IUsuarioQueries usuarioQueries)
    {
        _mediatorHandler = mediatorHandler;
        _usuarioQueries = usuarioQueries;
    }
    

    [HttpPost]
    public async Task<IActionResult> AdicionarPostagem(PostagemInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        if (!model.CategoriasId.Any())
        {
            AdicionarErro("Informe no mínimo uma categoria!");
            return CustomResponse();
        }

        var usuario = await _usuarioQueries.ObterPorId(model.UsuarioId);

        var comando = new AdicionarPostagemCommand(usuario.UsuarioId,
        usuario.Nome, usuario.Foto, model.Titulo, model.Descricao,
        model.CategoriasId);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }
}
