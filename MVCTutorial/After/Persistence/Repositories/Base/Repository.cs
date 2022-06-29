using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Domain.Base;
using Domain.RepositoryInterfaces.Generics;
using Domain.Search;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Generics;

namespace Persistence.Repositories.Base;

public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    protected Repository([NotNull] EfCoreContext context)
    {
        Context = context;
    }

    protected EfCoreContext Context  { get; }
    private   DbSet<TEntity>   Entities => Context.Set<TEntity>();

    private IQueryable<TEntity> EntityQuery => Entities;

    public virtual Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        return DefaultIncludes(EntityQuery)
              .Includes(includes)
              .ToListAsync();
    }

    public virtual async Task<IPartialCollection<TEntity>> GetAllAsync(SortOrder                          sortOrder,
                                                                       Expression<Func<TEntity, dynamic>> sortExpression,
                                                                       int                                offset,
                                                                       int                                limit, params Expression<Func<TEntity, object>>[] includes)
    {
        List<TEntity> entities;

        if (sortOrder == SortOrder.Ascending)
        {
            entities = await DefaultIncludes(EntityQuery)
                            .Includes(includes)
                            .OrderBy(sortExpression)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
        }
        else
        {
            entities = await DefaultIncludes(EntityQuery)
                            .Includes(includes)
                            .OrderByDescending(sortExpression)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
        }

        return new PartialCollection<TEntity>(entities, entities.Count, offset, limit);
    }

    public Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var queryable = EntityQuery.Where(predicate);

        return DefaultIncludes(queryable)
              .Includes(includes)
              .ToListAsync();
    }

    public async Task<IPartialCollection<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>>?           predicate,
                                                                         SortOrder                                  sortOrder,
                                                                         Expression<Func<TEntity, dynamic>>         sortExpression,
                                                                         int                                        offset,
                                                                         int                                        limit,
                                                                         params Expression<Func<TEntity, object>>[] includes)
    {
        var queryable = EntityQuery.Where(predicate);

        List<TEntity> entities;

        if (sortOrder == SortOrder.Ascending)
        {
            entities = await DefaultIncludes(queryable)
                            .Includes(includes)
                            .OrderBy(sortExpression)
                            .Skip(offset)
                            .Take(limit)
                            .AsNoTracking()
                            .ToListAsync();
        }
        else
        {
            entities = await DefaultIncludes(queryable)
                            .Includes(includes)
                            .OrderByDescending(sortExpression)
                            .Skip(offset)
                            .Take(limit)
                            .AsNoTracking()
                            .ToListAsync();
        }

        return new PartialCollection<TEntity>(entities, entities.Count, offset, limit);
    }

    public virtual Task<TEntity> GetByIdAsync(long id, params Expression<Func<TEntity, object>>[] includes)
    {
        var queryable = EntityQuery.Where(p => p.Id.Equals(id));

        return DefaultIncludes(queryable)
              .Includes(includes)
              .SingleOrDefaultAsync();
    }

    public virtual Task<TEntity> GetFirstIdAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var queryable = EntityQuery.Where(predicate);

        return DefaultIncludes(queryable)
              .Includes(includes)
              .FirstOrDefaultAsync();
    }

    public virtual Task<bool> ExistAsync(long id)
    {
        return Entities.AnyAsync(p => p.Id.Equals(id));
    }

    public Task<long> CountAsync()
    {
        return Entities.LongCountAsync();
    }

    public Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return Entities.LongCountAsync(predicate);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await Entities.AddAsync(entity);

        return result.Entity;
    }

    public async Task<IReadOnlyCollection<TEntity>> AddAsync(IEnumerable<TEntity> entities)
    {
        var returnEntities = new List<TEntity>();

        foreach (var entity in entities)
        {
            var returnEntity = await AddAsync(entity);
            returnEntities.Add(returnEntity);
        }

        return returnEntities;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = Entities.Update(entity).Entity;
        return Task.FromResult(updatedEntity);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    protected virtual IQueryable<TEntity> DefaultIncludes(IQueryable<TEntity> queryable)
    {
        return queryable;
    }
}

public static class QueryableExtension
{
    public static IQueryable<TEntity> Includes<TEntity>(this   IQueryable<TEntity>                 query,
                                                        params Expression<Func<TEntity, object>>[] includes) where TEntity : Entity
    {
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }
}