
using EasyTodoListApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyTodoListApp.Infrastructure.DatabaseContext;

public class EasyTodoListAppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public EasyTodoListAppDbContext() { }

    public EasyTodoListAppDbContext(DbContextOptions<EasyTodoListAppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=easytodolist.dat");

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.ToTable("Todo");
            entity.Property(e => e.Description).HasConversion(d => d.Value, d => Descriptor.CreateOrThrowArgException(d, Todo.IS_DSCRIPTION_REQUIRED, Todo.IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED, Todo.MAX_LENGTH_FOR_DESCRIPTION))
                .HasMaxLength(Todo.MAX_LENGTH_FOR_DESCRIPTION)
                .IsUnicode(false);
            entity.Property(e => e.Identifier).HasConversion(i => i.Value, i => Identifier<Todo>.Create(i));
            entity.ComplexProperty(e => e.Dates);
        });
}
