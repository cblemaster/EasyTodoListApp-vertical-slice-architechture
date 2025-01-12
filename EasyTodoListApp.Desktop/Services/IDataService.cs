
using EasyTodoListApp.Desktop.Models;

namespace EasyTodoListApp.Desktop.Services;

internal interface IDataService
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
