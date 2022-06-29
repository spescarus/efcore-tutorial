using System.Linq.Expressions;
using Domain.Base;
using Domain.Search;

namespace Domain.RepositoryInterfaces.Generics;

public interface IRepository<TEntity> where TEntity : Entity
{
    // Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    //
    // Task<IPartialCollection<TEntity>> GetAllAsync(SortOrder sortOrder,
    //                                               Expression<Func<TEntity, dynamic>> sortExpression,
    //                                               int offset,
    //                                               int limit, params Expression<Func<TEntity, object>>[] includes);
    // Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    // Task<TEntity> GetByIdAsync(long id, params Expression<Func<TEntity, object>>[] includes);
    // Task<TEntity> GetFirstIdAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> ExistAsync(long id);
    Task<long> CountAsync();
    Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<IReadOnlyCollection<TEntity>> AddAsync(IEnumerable<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    void Delete(TEntity entity);
}