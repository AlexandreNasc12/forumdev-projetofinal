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

    /// <summary>
    /// Obter todas as categorias existentes em nossa base de dados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var categorias = await _categoriasQueries.ObterTodas();

        return CustomResponse(categorias);
    }

    /// <summary>
    /// Adicione um nova categoria para utilização em novas postagens
    /// </summary>
    /// <param name="model">O nome de uma categoria deve conter no máximo 50 caracteres, sua descrição no máximo 5000 caracteres</param>
    /// <returns></returns>
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
