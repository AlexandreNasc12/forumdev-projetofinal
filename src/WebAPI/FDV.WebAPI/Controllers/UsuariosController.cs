using System;
using FDV.Usuarios.App.Domain.Interfaces;
using FDV.WebApi.Core.Controllers;
using FDV.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FDV.WebAPI.Controllers;

[Route("api/usuario")]
public class UsuariosController : MainController
{
    public readonly IUsuarioRepository _UsuarioRepository;

    public UsuariosController(IUsuarioRepository usuarioRepository)
    {
        _UsuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var usuarios = await _UsuarioRepository.ObterTodos();

        var usuarioViews = usuarios.Select(UsuarioViewModel.Mapear);

        if (!usuarioViews.Any())
        {
            AdicionarErro("Não encontrei usuário na base de dados!");
            return CustomResponse();
        }

        return CustomResponse(usuarioViews);
    }
}
