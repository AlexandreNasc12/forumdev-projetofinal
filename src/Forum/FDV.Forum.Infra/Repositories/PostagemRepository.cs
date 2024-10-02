using System;
using System.Data.Common;
using FDV.Core.Data;
using FDV.Forum.Domain;
using FDV.Forum.Domain.Interfaces;
using FDV.Forum.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace FDV.Forum.Infra.Repositories;

public class PostagemRepository : IPostagemRepository
{
    private readonly PostagensContext _context;

    public PostagemRepository(PostagensContext context)
    {
        _context = context;
    }

    public IUnitOfWorks UnitOfWork => _context;

    public void Adicionar(Postagem entity)
    {
        _context.Postagens.Add(entity);
    }

    public void Apagar(Func<Postagem, bool> predicate)
    {
        var postagens = _context.Postagens.Where(predicate).ToList();

        _context.Postagens.RemoveRange(postagens);
    }

    public void Atualizar(Postagem entity)
    {
        _context.Postagens.Update(entity);
    }

    public async Task<IEnumerable<Postagem>> ObterModeracao()
    {
        return await _context.Postagens.Include(c => c.Categorias)
            .Where(c => !c.Aprovado).ToListAsync();
    }

    public async Task<Postagem> ObterPorId(Guid Id)
    {
        return await _context.Postagens.Include(c => c.Categorias)
        .FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<IEnumerable<Postagem>> ObterPublicadas()
    {
        return await _context.Postagens.Include(c => c.Categorias)
                .Where(c => c.Publicado).ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategorias()
    {
        return await _context.Categorias.OrderBy(c => c.Nome).ToListAsync();
    }

    public void Adicionar(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }

    public async Task<Categoria> ObterCategoriaPorId(Guid Id)
    {
        return await _context.Categorias.FindAsync(Id);
    }

    public async Task<Categoria> ObterCategoriaPorHash(Guid hash)
    {
        return await _context.Categorias.FirstOrDefaultAsync(c => c.Hash == hash);
    }

    public async Task<IEnumerable<Categoria>> ObterCategorias(Guid[] categoriaIds)
    {
        return await _context.Categorias
                            .Where(c => categoriaIds.Contains(c.Id))
                            .ToListAsync();
    }

    public DbConnection ObterConexao()
    {
        return _context.Database.GetDbConnection();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
