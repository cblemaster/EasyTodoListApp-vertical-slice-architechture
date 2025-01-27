﻿using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;

namespace EasyTodoListApp.Desktop.Interfaces;

public interface IDataService
{
    Task<DataServiceResponse<string>> TryCreateTodoAsync(CreateTodoDTO dto);
    Task<DataServiceResponse<string>> TryUpdateTodoAsync(UpdateTodoDTO dto, Guid id);
    Task<DataServiceResponse<string>> TryMarkTodoIncompleteAsync(MarkTodoIncompleteDTO dto, Guid id);
    Task<DataServiceResponse<string>> TryDeleteTodoAsync(Guid id);
    Task<IEnumerable<TodoDTO>> GetAllTodosCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosDueTodayAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosImportantAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosNotCompleteAsync();
    Task<IEnumerable<TodoDTO>> GetAllTodosOverdueAsync();
    Task<DataServiceResponse<TodoDTO>> TryGetTodoByIdOrThrowHttpExAsync(Guid id);
}
