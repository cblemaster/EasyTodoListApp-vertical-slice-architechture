using EasyTodoListApp.Domain;

namespace EasyTodoListApp.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAllTodosComplete();
        IEnumerable<Todo> GetAllTodosNotComplete();
        Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id);
    }
}
