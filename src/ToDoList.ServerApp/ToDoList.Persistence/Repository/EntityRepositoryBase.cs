using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Common.Entities;

namespace ToDoList.Persistence.Repository;

public abstract class EntityRepositoryBase<TEntity, TContext>(TContext dbContext)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected async ValueTask<IList<TEntity>> GetAllAsync(bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.ToListAsync();
    }

    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery.AsNoTracking();

        return await initialQuery.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken: cancellationToken);
    }

    protected async ValueTask<IList<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.Where(entity => ids.Contains(entity.Id))
            .ToListAsync(cancellationToken: cancellationToken);
    }

    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken: cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity> DeleteAsync(TEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity;
    }

    protected async ValueTask<TEntity> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var found = await GetByIdAsync(id, true, cancellationToken: cancellationToken) ??
                    throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(found);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return found;
    }
}