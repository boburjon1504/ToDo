using System.Linq.Expressions;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.ToDos.Services;

public interface IToDoService
{
    IEnumerable<ToDoEntity> Get(Expression<Func<ToDoEntity, bool>>? predicate = default, bool asNoTracking = false);
    
    ValueTask<IList<ToDoEntity>> GetAllAsync(bool asNoTracking = false);
    
    ValueTask<ToDoEntity?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<IList<ToDoEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<ToDoEntity> CreateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default);
    
    ValueTask<bool> UpdateAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default);
    
    ValueTask<ToDoEntity> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default);
    
    ValueTask<ToDoEntity> DeleteAsync(ToDoEntity entity, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}