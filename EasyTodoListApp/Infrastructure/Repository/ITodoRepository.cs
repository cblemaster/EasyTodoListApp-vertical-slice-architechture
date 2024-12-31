
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAllTodosComplete();
        IEnumerable<Todo> GetAllTodosNotComplete();
        Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id);
        Task DeleteTodoAsync(Identifier<Todo> id);
        Task CreateTodoAsync(CreateTodoCommand command);
        Task ToggleTodoImportanceAsync(ToggleTodoImportanceCommand command);
        Task ToggleTodoCompletionAsync(ToggleTodoCompletionCommand command);
    }
}
