using System;
using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.Forum.App.Queries;
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
    private readonly IPostagensQueries _postagemQueries;

    public PostagensController(IMediatorHandler mediatorHandler, 
    IPostagensQueries postagemQueries)
    {
        _mediatorHandler = mediatorHandler;
        _postagemQueries = postagemQueries;
    }

    [HttpGet]
    public async Task<IActionResult> ObterPublicadas()
    {
        var postagens = await _postagemQueries.ObterTodas();

        return CustomResponse(postagens);
    }


    [HttpPatch("{Id:Guid}")]
    public async Task<IActionResult> Moderacao(Guid Id, ModeracaoInputModel model)
    {
        var comando = new ModerarPostagemCommand(model.Publicado,model.Aprovado,Id);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
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

    [HttpPost("comentar")]
    public async Task<IActionResult> Comentar(ComentarioInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuario = await _usuarioQueries.ObterPorId(model.UsuarioId);

        var comando = new ComentarCommand(model.PostagemId,usuario.UsuarioId,usuario.Nome,usuario.Foto,model.Descricao);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }
}   
