using System;
using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.Forum.App.Queries;
using FDV.WebApi.Core.Controllers;
using FDV.WebAPI.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FDV.WebAPI.Controllers;

[Route("api/categorias")]
public class CategoriasController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly ICategoriasQueries _categoriasQueries;

    public CategoriasController(IMediatorHandler mediatorHandler,
                                ICategoriasQueries categoriasQueries)
    {
        _mediatorHandler = mediatorHandler;
        _categoriasQueries = categoriasQueries;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var categorias = await _categoriasQueries.ObterTodas();

        return CustomResponse(categorias);
    }


    [HttpPost]
    public async Task<IActionResult> Adicionar(CategoriaInputModel model)
    {
        var comando = new AdicionarCategoriaCommand(model.Nome, model.Descricao);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }

    [HttpPut("{Id:Guid}")]
    public async Task<IActionResult> Atualizar(Guid Id, CategoriaInputModel model)
    {
        var comando = new AtualizarCategoriaCommand(Id, model.Nome, model.Descricao);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }
}
