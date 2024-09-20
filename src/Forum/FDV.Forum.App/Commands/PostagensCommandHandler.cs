using System;
using FDV.Core.Messages;
using FDV.Forum.Domain;
using FDV.Forum.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace FDV.Forum.App.Commands;

public class PostagensCommandHandler : CommandHandler,
            IRequestHandler<AdicionarCategoriaCommand, ValidationResult>,
            IRequestHandler<AtualizarCategoriaCommand, ValidationResult>,
            IRequestHandler<AdicionarPostagemCommand, ValidationResult>, IDisposable
{

    private readonly IPostagemRepository _postagemRepository;

    public PostagensCommandHandler(IPostagemRepository postagemRepository)
    {
        _postagemRepository = postagemRepository;
    }

    public async Task<ValidationResult> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        if (await CategoriaExiste(request.Nome))
        {
            AdicionarErro("Esta categoria já existe no sistema!");
            return ValidationResult;
        }

        var nova = new Categoria(request.Nome, request.Descricao);

        _postagemRepository.Adicionar(nova);

        return await PersistirDados(_postagemRepository.UnitOfWork);
    }

    private async Task<bool> CategoriaExiste(string categoriaNome)
    {
        var hash = Categoria.GerarHash(categoriaNome);

        var categoriaEncontrada = await _postagemRepository.ObterCategoriaPorHash(hash);

        return categoriaEncontrada != null;
    }

    public async Task<ValidationResult> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        if (await CategoriaExiste(request.Nome))
        {
            AdicionarErro("Esta categoria já existe no sistema!");
            return ValidationResult;
        }

        var categoriaEncontrada = await _postagemRepository.ObterCategoriaPorId(request.CategoriaId);

        if (categoriaEncontrada is null)
        {
            AdicionarErro("Categoria não encontrada!");
            return ValidationResult;
        }

        categoriaEncontrada.AtribuirNome(request.Nome);
        categoriaEncontrada.AtribuirDescricao(request.Descricao);

        _postagemRepository.Atualizar(categoriaEncontrada);

        return await PersistirDados(_postagemRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AdicionarPostagemCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Usuario(request.Id, request.Nome, request.Foto);

        var categorias = await _postagemRepository.ObterCategorias(request.CategoriaIds);

        var postagem = new Postagem(usuario,request.Titulo,request.Descricao);

        foreach (var categoria in categorias)
        {
            postagem.Adicionar(categoria);
        }

        _postagemRepository.Adicionar(postagem);

        return await PersistirDados(_postagemRepository.UnitOfWork);
    }

    public void Dispose()
    {
        _postagemRepository.Dispose();
    }
}
