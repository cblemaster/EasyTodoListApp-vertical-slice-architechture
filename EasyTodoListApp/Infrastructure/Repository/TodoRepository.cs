
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.API.Todos.Validation;
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.DatabaseContext;

namespace EasyTodoListApp.Infrastructure.Repository;

public class TodoRepository(EasyTodoListAppDbContext context)
{
   private readonly EasyTodoListAppDbContext _context = context;

   #region commands
   public async Task CreateTodoAsync(CreateTodoCommand command)
   {
      if (command is null)
      {
         //return bad request w/error
      }

      (bool IsValid, string ErrorMessage) = ValidateCreateTodoCommand.Validate(command);
      if (!IsValid)
      {
         // TODO: return 'bad request with error';
      }
      else
      {
         Todo entity = Todo.Create(command.Description, command.DueDate, command.IsImportant, command.IsComplete);
         _context.Set<Todo>().Add(entity);
         await _context.SaveChangesAsync();
         // return success 
      }
   }
   public async Task DeleteTodoAsync(Guid id)
   {
      if (await GetTodoByIdOrNullAsync(id) is not Todo entity)
      {
         // TODO: return 'not found';
      }
      else if (entity.IsImportant)
      {
         // TODO: return 'bad request with error';
      }
      else
      {
         _context.Set<Todo>().Remove(entity);
         await _context.SaveChangesAsync();
         // TODO: return success response
      }

   }
   public async Task ToggleTodoCompletionAsync(Guid id)
   {
      if (await GetTodoByIdOrNullAsync(id) is not Todo entity)
      {
         // TODO: return 'not found';
      }
      else
      {
         entity.SetIsComplete(!entity.IsComplete);
         await _context.SaveChangesAsync();
         // TODO: return success response
      }
   }
   public async Task ToggleTodoImportanceAsync(Guid id)
   {
      if (await GetTodoByIdOrNullAsync(id) is not Todo entity)
      {
         // TODO: return 'not found';
      }
      else if (entity.IsComplete)
      {
         // TODO: return 'bad request with error';
      }
      else
      {
         entity.SetIsImportant(!entity.IsImportant);
         await _context.SaveChangesAsync();
         // TODO: return success response
      }

   }
   public async Task UpdateTodo(UpdateTodoCommand command, Guid id)
   {
      if (await GetTodoByIdOrNullAsync(id) is not Todo entity)
      {
         // TODO: return 'not found';
      }
      else if (entity.IsComplete)
      {
         // TODO: return 'bad request with error';
      }
      else
      {
         entity.SetDescription(command.Description);
         entity.SetDueDate(command.DueDate);
         //entity.Dates = entity.Dates with { UpdateDate = DateTime.Now };
         await _context.SaveChangesAsync();
      }
      // otherwise update description and/or due date if they have changed, save changes and return an ok/success result
   }
   #endregion commands

   #region queries
   public IEnumerable<Todo> GetAllTodosComplete() => _context.Set<Todo>().Where(t => t.IsComplete);
   public IEnumerable<Todo> GetAllTodosNotComplete() => _context.Set<Todo>().Where(t => !t.IsComplete);
   public IEnumerable<Todo> GetAllTodosDueToday() => GetAllTodosNotComplete().Where(t => t.DueDate.HasValue && t.DueDate.Value == DateOnly.FromDateTime(DateTime.Today));
   public IEnumerable<Todo> GetAllTodosImportant() => GetAllTodosNotComplete().Where(t => t.IsImportant);
   public IEnumerable<Todo> GetAllTodosOverdue() => GetAllTodosNotComplete().Where(t => t.DueDate.HasValue && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today));
   public async Task<Todo?> GetTodoByIdOrNullAsync(Guid id) => await _context.Set<Todo>().FindAsync(id);
   #endregion queries
}
