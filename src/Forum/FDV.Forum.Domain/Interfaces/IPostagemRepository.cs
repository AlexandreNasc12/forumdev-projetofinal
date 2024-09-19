using System;
using FDV.Core.Data;

namespace FDV.Forum.Domain.Interfaces;

public interface IPostagemRepository : IRepository<Postagem>, IDisposable
{
    Task<IEnumerable<Postagem>> ObterPublicadas();

    Task<IEnumerable<Postagem>> ObterModeracao();

    Task<IEnumerable<Categoria>> ObterCategorias();

    Task<Categoria> ObterCategoriaPorId(Guid Id);

    void Adicionar(Categoria categoria);
    
    void Atualizar(Categoria categoria);
}
