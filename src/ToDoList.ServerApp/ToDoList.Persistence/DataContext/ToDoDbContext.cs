using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Persistence.DataContext;

public class ToDoDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ToDoEntity> ToDos => Set<ToDoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}