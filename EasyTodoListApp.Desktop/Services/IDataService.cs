
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;

namespace EasyTodoListApp.Desktop.Services;

public interface IDataService
{
    Task<DataServiceResponse<string>> TryCreateTodoAsync(CreateTodoDTO dto);
    Task<DataServiceResponse<string>> TryDeleteTodoAsync(Guid id);
    Task<IEnumerable<TodoDTO>> GetAllTodosCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosDueTodayAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosImportantAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosNotCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosOverdueAsync();
    Task<DataServiceResponse<TodoDTO>> TryGetTodoByIdOrThrowHttpExAsync(Guid id);
    Task<DataServiceResponse<string>> TryUpdateTodoAsync(UpdateTodoDTO dto, Guid id);
}
