
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.DatabaseContext;

namespace EasyTodoListApp.Infrastructure.Repository;

public class TodoRepository(EasyTodoListAppDbContext context) : ITodoRepository
{
    private readonly EasyTodoListAppDbContext _context = context;

    public async Task CreateTodoAsync(CreateTodoCommand command)
    {
        _context.Set<Todo>().Add(command.NewTodo!);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateTodoAsync(UpdateTodoCommand command)
    {
        Todo entity = (await GetTodoByIdOrNullAsync(Identifier<Todo>.Create(command.Id)))!;  // handler has verified that the entity exists
        entity.SetDescription(command.Description);
        entity.SetDueDate(command.DueDate);
        entity.SetIsImportant(command.IsImportant);
        entity.SetIsComplete(command.IsComplete);
        entity.SetUpdateDate();
        await _context.SaveChangesAsync();
    }
    public async Task DeleteTodoAsync(Identifier<Todo> id)
    {
        Todo entity = (await GetTodoByIdOrNullAsync(id))!;  // handler has verified that the entity exists
        _context.Set<Todo>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Todo> GetAllTodosComplete() => _context.Set<Todo>().Where(t => t.IsComplete);
    public IEnumerable<Todo> GetAllTodosNotComplete() => _context.Set<Todo>().Where(t => !t.IsComplete);
    public async Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id) => await _context.Set<Todo>().FindAsync(id);
}
