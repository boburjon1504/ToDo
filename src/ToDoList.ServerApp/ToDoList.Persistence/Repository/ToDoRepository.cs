using System.Linq.Expressions;
using ToDoList.Domain.Entities;
using ToDoList.Persistence.DataContext;
using ToDoList.Persistence.Repository.Interfaces;

namespace ToDoList.Persistence.Repository;

public class ToDoRepository(ToDoDbContext context)
    : EntityRepositoryBase<ToDoEntity, ToDoDbContext>(context), IToDoRepository
{
    public IQueryable<ToDoEntity> Get(Expression<Func<ToDoEntity, bool>>? predicate = default,
        bool asNoTracking = false) =>
        base.Get(predicate, asNoTracking);

    public new ValueTask<IList<ToDoEntity>> GetAllAsync(bool asNoTracking = false) =>
        base.GetAllAsync(asNoTracking);

    public new ValueTask<ToDoEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdAsync(id, asNoTracking, cancellationToken);

    public new ValueTask<IList<ToDoEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        base.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public new ValueTask<ToDoEntity> CreateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.CreateAsync(entity, saveChanges, cancellationToken);

    public new ValueTask<ToDoEntity> UpdateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.UpdateAsync(entity, saveChanges, cancellationToken);

    public new ValueTask<ToDoEntity> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteByIdAsync(id, saveChanges, cancellationToken);

    public new ValueTask<ToDoEntity> DeleteAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        base.DeleteAsync(entity, saveChanges, cancellationToken);
}