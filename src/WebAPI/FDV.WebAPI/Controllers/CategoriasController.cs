using System;
using FDV.Core.Mediator;
using FDV.Forum.App.Commands;
using FDV.WebApi.Core.Controllers;
using FDV.WebAPI.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FDV.WebAPI.Controllers;

[Route("api/categorias")]
public class CategoriasController : MainController
{
    private readonly IMediatorHandler _mediatorHandler;

    public CategoriasController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar(CategoriaInputModel model)
    {
        var comando = new AdicionarCategoriaCommand(model.Nome,model.Descricao);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }
}
