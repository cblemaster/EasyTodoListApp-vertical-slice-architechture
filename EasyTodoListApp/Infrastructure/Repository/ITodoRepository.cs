
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        Task CreateTodoAsync(CreateTodoCommand command);
        Task UpdateTodoAsync(UpdateTodoCommand command);
        Task DeleteTodoAsync(Identifier<Todo> id);

        IEnumerable<Todo> GetAllTodosComplete();
        IEnumerable<Todo> GetAllTodosNotComplete();
        Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id);
    }
}
