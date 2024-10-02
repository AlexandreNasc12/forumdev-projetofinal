using System;
using System.Data.Common;
using FDV.Core.Data;

namespace FDV.Forum.Domain.Interfaces;

public interface IPostagemRepository : IRepository<Postagem>, IDisposable
{
    Task<IEnumerable<Postagem>> ObterPublicadas();

    Task<IEnumerable<Postagem>> ObterModeracao();

    Task<IEnumerable<Categoria>> ObterCategorias();

    Task<IEnumerable<Categoria>> ObterCategorias(Guid[] categoriaIds);

    Task<Categoria> ObterCategoriaPorId(Guid Id);

    Task<Categoria> ObterCategoriaPorHash(Guid hash);

    DbConnection ObterConexao();

    void Adicionar(Categoria categoria);

    void Atualizar(Categoria categoria);
}
