
using EasyTodoListApp.UI.Desktop.Models;

namespace EasyTodoListApp.UI.Desktop.Services
{
    public interface IHttpDataService
    {
        void CreateTodoAsync(CreateTodoCommand command);
        void DeleteTodoAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllTodosCompleteAsync();
        Task<IEnumerable<Todo>> GetAllTodosDueTodayAsync();
        Task<IEnumerable<Todo>> GetAllTodosImportantAsync();
        Task<IEnumerable<Todo>> GetAllTodosNotCompleteAsync();
        Task<IEnumerable<Todo>> GetAllTodosOverdueAsync();
        Task<Todo> GetTodoByIdOrThrowHttpExAsync(Guid id);
        void ToggleTodoCompletionAsync(Guid id);
        void ToggleTodoImportanceAsync(Guid id);
        void UpdateTodoAsync(UpdateTodoCommand command, Guid id);
    }
}
