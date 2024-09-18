using System;
using FDV.Usuarios.App.Application.ViewModels;
using FDV.Usuarios.App.Domain.Interfaces;

namespace FDV.Usuarios.App.Application.Queries;

public interface IUsuarioQueries
{
    Task<IEnumerable<UsuarioViewModel>> ObterTodos();
}


public class UsuarioQueries : IUsuarioQueries
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioQueries(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioViewModel>> ObterTodos()
    {
        var usuarios = await _usuarioRepository.ObterTodos();

        return usuarios.Select(UsuarioViewModel.Mapear);
    }
}
