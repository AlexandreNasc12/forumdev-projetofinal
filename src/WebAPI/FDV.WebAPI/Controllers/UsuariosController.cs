using System;
using FDV.Core.Mediator;
using FDV.Core.Ultilities;
using FDV.Usuarios.App.Application.Commands;
using FDV.Usuarios.App.Application.Queries;
using FDV.WebApi.Core.Controllers;
using FDV.WebAPI.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FDV.WebAPI.Controllers;

[Route("api/usuario")]
public class UsuariosController : MainController
{
    public readonly IUsuarioQueries _usuarioQueries;

    public readonly IMediatorHandler _mediatorHandler;

    public UsuariosController(IMediatorHandler mediatorHandler, IUsuarioQueries usuarioQueries)
    {

        _mediatorHandler = mediatorHandler;
        _usuarioQueries = usuarioQueries;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _usuarioQueries.ObterTodos();

        return CustomResponse(usuarios);
    }


    [HttpPost]
    public async Task<IActionResult> Adicionar(UsuarioInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var dataDeNascimento = model.DataDeNascimento.ConverterParaData();

        if (dataDeNascimento is null)
        {
            AdicionarErro("Data de nascimento inválida!");
            return CustomResponse();
        }

        var comando = new AdicionarUsuarioCommand(model.Nome, model.Cpf, dataDeNascimento!.Value, model.Foto, model.Email, model.Senha);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }

    [HttpPost("endereco/{Id:Guid}")]
    public async Task<IActionResult> AdicionarEndereco(Guid Id, EnderecoInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var comando = new AdicionarEnderecoCommand(Id,model.Logradouro,model.Numero,
        model.Complemento,model.Cep,model.Bairro,
        model.Cidade,model.Estado);

        var result = await _mediatorHandler.EnviarComando(comando);

        return CustomResponse(result);
    }
}
