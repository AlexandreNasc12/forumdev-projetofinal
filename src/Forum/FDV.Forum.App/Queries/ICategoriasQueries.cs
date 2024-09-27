using System;
using FDV.Forum.App.ViewModels;
using FDV.Forum.Domain.Interfaces;

namespace FDV.Forum.App.Queries;

public interface ICategoriasQueries : IDisposable
{
    Task<IEnumerable<CategoriaViewModel>> ObterTodas();
}

public class CategoriasQueries : ICategoriasQueries
{
    private readonly IPostagemRepository _postagemRepository;

    public CategoriasQueries(IPostagemRepository postagemRepository)
    {
        _postagemRepository = postagemRepository;
    }

    public async Task<IEnumerable<CategoriaViewModel>> ObterTodas()
    {
        var categorias = await _postagemRepository.ObterCategorias();

        return categorias.Select(CategoriaViewModel.Mapear);
    }

    public void Dispose()
    {
        _postagemRepository.Dispose();
    }
}