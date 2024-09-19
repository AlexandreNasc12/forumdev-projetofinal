using System;
using FDV.Core.Messages;
using FDV.Forum.Domain;
using FDV.Forum.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace FDV.Forum.App.Commands;

public class PostagensCommandHandler : CommandHandler,
            IRequestHandler<AdicionarCategoriaCommand, ValidationResult>,
            IRequestHandler<AtualizarCategoriaCommand, ValidationResult>, IDisposable
{

    private readonly IPostagemRepository _postagemRepository;

    public PostagensCommandHandler(IPostagemRepository postagemRepository)
    {
        _postagemRepository = postagemRepository;
    }

    public async Task<ValidationResult> Handle(AdicionarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var nova = new Categoria(request.Nome, request.Descricao);

        _postagemRepository.Adicionar(nova);

        return await PersistirDados(_postagemRepository.UnitOfWork);
    }


    public async Task<ValidationResult> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

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

    public void Dispose()
    {
        _postagemRepository.Dispose();
    }
}
