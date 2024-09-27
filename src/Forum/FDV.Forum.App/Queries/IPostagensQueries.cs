using System;
using FDV.Forum.App.ViewModels;
using FDV.Forum.Domain.Interfaces;

namespace FDV.Forum.App.Queries;

public interface IPostagensQueries : IDisposable
{
    Task<IEnumerable<PostagemViewModel>> ObterTodas();
}

public class PostagensQueries : IPostagensQueries
{
    private readonly IPostagemRepository _postagemRepository;

    public PostagensQueries(IPostagemRepository postagemRepository)
    {
        _postagemRepository = postagemRepository;
    }

    public async Task<IEnumerable<PostagemViewModel>> ObterTodas()
    {
        var postagens = await _postagemRepository.ObterPublicadas();

        return postagens.Select(PostagemViewModel.Mapear);
    }

    public void Dispose()
    {
        _postagemRepository.Dispose();
    }
}
