
using EasyTodoListApp.UI.Desktop.Models;

namespace EasyTodoListApp.UI.Desktop.Services
{
    public interface IHttpDataService
    {
        void CreateTodoAsync(CreateTodoDTO command);
        void DeleteTodoAsync(Guid id);
        Task<IEnumerable<TodoDTO>> GetAllTodosCompleteAsync();
        Task<IEnumerable<TodoDTO>> GetAllTodosDueTodayAsync();
        Task<IEnumerable<TodoDTO>> GetAllTodosImportantAsync();
        Task<IEnumerable<TodoDTO>> GetAllTodosNotCompleteAsync();
        Task<IEnumerable<TodoDTO>> GetAllTodosOverdueAsync();
        Task<TodoDTO> GetTodoByIdOrThrowHttpExAsync(Guid id);
        void ToggleTodoCompletionAsync(Guid id);
        void ToggleTodoImportanceAsync(Guid id);
        void UpdateTodoAsync(UpdateTodoDTO command, Guid id);
    }
}
