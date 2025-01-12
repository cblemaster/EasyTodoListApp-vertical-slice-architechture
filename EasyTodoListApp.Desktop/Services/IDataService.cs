
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;

namespace EasyTodoListApp.Desktop.Services;

public interface IDataService
{
    Task<DataServiceResponse<string>> CreateTodoAsync(CreateTodoDTO dto);
    Task<DataServiceResponse<string>> DeleteTodoAsync(Guid id);
    Task<IEnumerable<TodoDTO>> GetAllTodosCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosDueTodayAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosImportantAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosNotCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosOverdueAsync();
    Task<DataServiceResponse<TodoDTO>> GetTodoByIdOrThrowHttpExAsync(Guid id);
    Task<DataServiceResponse<string>> ToggleTodoCompletionAsync(Guid id);
    Task<DataServiceResponse<string>> ToggleTodoImportanceAsync(Guid id);
    Task<DataServiceResponse<string>> UpdateTodoAsync(UpdateTodoDTO dto, Guid id);
}
