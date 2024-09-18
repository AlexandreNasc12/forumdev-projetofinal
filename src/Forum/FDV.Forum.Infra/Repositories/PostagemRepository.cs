using System;
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
        return await _context.Postagens
            .Where(c => !c.Aprovado).ToListAsync();
    }

    public async Task<Postagem> ObterPorId(Guid Id)
    {
        return await _context.Postagens.FindAsync(Id);
    }

    public async Task<IEnumerable<Postagem>> ObterPublicadas()
    {
        return await _context.Postagens
                .Where(c => c.Publicado).ToListAsync();
    }

    public void Adicionar(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
