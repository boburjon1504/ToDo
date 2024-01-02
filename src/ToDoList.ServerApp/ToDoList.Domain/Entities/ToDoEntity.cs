using ToDoList.Domain.Common.Entities;

namespace ToDoList.Domain.Entities;

public class ToDoEntity : AuditableEntity
{
    public string Title { get; set; } = default!;

    public bool IsDone { get; set; }

    public bool IsFavorite { get; set; }

    public DateTimeOffset DueTime { get; set; }

    public DateTimeOffset ReminderTime { get; set; }
}