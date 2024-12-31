
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        Task CreateTodoAsync(CreateTodoCommand command);
        Task UpdateTodoAsync(UpdateTodoCommand command);
        Task ToggleTodoImportanceAsync(ToggleTodoImportanceCommand command);
        Task ToggleTodoCompletionAsync(ToggleTodoCompletionCommand command);
        Task DeleteTodoAsync(Identifier<Todo> id);

        IEnumerable<Todo> GetAllTodosComplete();
        IEnumerable<Todo> GetAllTodosNotComplete();
        Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id);
    }
}
