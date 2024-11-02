using System.Linq.Expressions;
using Core.Entities;

namespace Core.Repositories;

public interface IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
{
    List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null, bool enableAutoInclude = true);
    TEntity? GetById(TId id);
    TEntity? Add(TEntity entity);
    TEntity? Update(TEntity entity);
    TEntity? Remove(TEntity entity);
}