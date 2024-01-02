using System.Linq.Expressions;
using ToDoList.Application.ToDos.Services;
using ToDoList.Domain.Entities;
using ToDoList.Persistence.Repository.Interfaces;

namespace ToDoList.Infrastructure.ToDos.Services;

public class ToDoService(IToDoRepository repository) : IToDoService
{
    public IEnumerable<ToDoEntity> Get(Expression<Func<ToDoEntity, bool>>? predicate = default,
        bool asNoTracking = false) =>
        repository.Get(predicate, asNoTracking);

    public ValueTask<IList<ToDoEntity>> GetAllAsync(bool asNoTracking = false) =>
        repository.GetAllAsync(asNoTracking);

    public ValueTask<ToDoEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        repository.GetByIdAsync(id, asNoTracking, cancellationToken);

    public ValueTask<IList<ToDoEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default) =>
        repository.GetByIdsAsync(ids, asNoTracking, cancellationToken);

    public ValueTask<ToDoEntity> CreateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedTime = DateTime.UtcNow;
        
        return repository.CreateAsync(entity, saveChanges, cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {        
        entity.ModifiedTime = DateTimeOffset.UtcNow;
        
        await repository.UpdateAsync(entity, saveChanges, cancellationToken);

        return true;
    }

    public ValueTask<ToDoEntity> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        repository.DeleteByIdAsync(id, saveChanges, cancellationToken);

    public ValueTask<ToDoEntity> DeleteAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        repository.DeleteAsync(entity, saveChanges, cancellationToken);
}