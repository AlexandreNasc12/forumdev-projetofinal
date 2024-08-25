using FDV.Core.DomainObjects;

namespace FDV.Core.Data;

public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
{
    IUnitOfWorks UnitOfWork { get; }

    Task<TEntity> ObterPorId(Guid Id);

    void Adicionar(TEntity entity);
    
    void Atualizar(TEntity entity);

    void Apagar(Func<TEntity,bool> predicate);
}
