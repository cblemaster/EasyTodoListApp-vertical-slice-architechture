
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.DatabaseContext;

namespace EasyTodoListApp.Infrastructure.Repository;

public class TodoRepository(EasyTodoListAppDbContext context) : ITodoRepository
{
    private readonly EasyTodoListAppDbContext _context = context;

    #region commands
    public async Task CreateTodoAsync(CreateTodoCommand command)
    {
        _context.Set<Todo>().Add(command.NewTodo);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteTodoAsync(Identifier<Todo> id)
    {
        Todo? entity = await GetTodoByIdOrNullAsync(id);
        if (entity is not null)
        {
            _context.Set<Todo>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task ToggleTodoImportanceAsync(ToggleTodoImportanceCommand command)
    {
        Todo entity = await GetTodoByIdOrNullAsync(command.Id);
        entity.SetIsImportant(!entity.IsImportant);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleTodoCompletionAsync(ToggleTodoCompletionCommand command)
    {
        Todo entity = await GetTodoByIdOrNullAsync(command.Id);
        entity.SetIsComplete(!entity.IsComplete);
        await _context.SaveChangesAsync();
    }
    //public async Task UpdateTodo(UpdateTodoCommand command, Guid id)
    //{
    //   if (await GetTodoByIdOrNullAsync(id) is not Todo entity)
    //   {
    //      // TODO: return 'not found';
    //   }
    //   else if (entity.IsComplete)
    //   {
    //      // TODO: return 'bad request with error';
    //   }
    //   else
    //   {
    //      entity.SetDescription(command.Description);
    //      entity.SetDueDate(command.DueDate);
    //      //entity.Dates = entity.Dates with { UpdateDate = DateTime.Now };
    //      await _context.SaveChangesAsync();
    //   }
    //   // otherwise update description and/or due date if they have changed, save changes and return an ok/success result
    //}
    #endregion commands

    #region queries


    public IEnumerable<Todo> GetAllTodosComplete() => _context.Set<Todo>().Where(t => t.IsComplete);
    public IEnumerable<Todo> GetAllTodosNotComplete() => _context.Set<Todo>().Where(t => !t.IsComplete);
    //public IEnumerable<Todo> GetAllTodosDueToday() => GetAllTodosNotComplete().Where(t => t.DueDate.HasValue && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today));
    //public IEnumerable<Todo> GetAllTodosImportant() => GetAllTodosNotComplete().Where(t => t.IsImportant);
    //public IEnumerable<Todo> GetAllTodosOverdue() => GetAllTodosNotComplete().Where(t => t.DueDate.HasValue && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today));
    public async Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id) => await _context.Set<Todo>().FindAsync(id.Value);
    #endregion queries
}
