using EasyTodoListApp.UI.Desktop.Models;

namespace EasyTodoListApp.UI.Desktop.Services
{
    public interface IHttpDataService
    {
        void CreateTodo(CreateTodoCommand command);
        void DeleteTodo(Guid id);
        Task<IEnumerable<Todo>> GetAllTodosCompleteAsync();
        Task<IEnumerable<Todo>> GetAllTodosDueTodayAsync();
        Task<IEnumerable<Todo>> GetAllTodosImportantAsync();
        Task<IEnumerable<Todo>> GetAllTodosNotCompleteAsync();
        Task<IEnumerable<Todo>> GetAllTodosOverdueAsync();
        Task<Todo> GetTodoByIdAsync(Guid id);
        void ToggleTodoCompletion(Guid id);
        void ToggleTodoImportance(Guid id);
        void UpdateTodo(UpdateTodoCommand command, Guid id);
    }
}