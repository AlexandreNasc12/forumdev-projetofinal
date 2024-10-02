using System;
using Dapper;
using FDV.Forum.App.ViewModels;
using FDV.Forum.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace FDV.Forum.App.Queries;

public interface IPostagensQueries : IDisposable
{
    Task<IEnumerable<PostagemViewModel>> ObterTodas();

    Task<IEnumerable<PostagemViewModel>> ObterModeracao();

    Task<IEnumerable<ComentarioViewModel>> ObterComentarios(Guid postagemId);
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


    public async Task<IEnumerable<PostagemViewModel>> ObterModeracao()
    {
        var postagens = await _postagemRepository.ObterModeracao();

        return postagens.Select(PostagemViewModel.Mapear);
    }

    public async Task<IEnumerable<ComentarioViewModel>> ObterComentarios(Guid postagemId)
    {
        var SQL = @"SELECT 
                    UsuarioId,
                    UsuarioNome as 'Nome',
                    UsuarioFoto as 'Foto',
                    Descricao,
                    CONVERT(varchar,DataDeCadastro,103) as 'DataDeCadastro'
                FROM Comentario
                WHERE PostagemId = @Id
                ORDER BY DataDeCadastro DESC";

        using(var connection = new SqlConnection(_postagemRepository.ObterConexao().ConnectionString))
        {
            return await connection.QueryAsync<ComentarioViewModel>(SQL,new {Id = postagemId});
        }
    }

    public void Dispose()
    {
        _postagemRepository.Dispose();
    }

}
