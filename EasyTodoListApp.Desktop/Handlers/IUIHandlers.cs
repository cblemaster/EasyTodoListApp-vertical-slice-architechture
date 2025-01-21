﻿
namespace EasyTodoListApp.Desktop.Handlers;

public interface IUIHandlers
{
    Task TryHandleCreateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete);
    Task TryHandleDeleteTodoAsync(Guid id);
    Task TryHandleUpdateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete, Guid id);
    Task TryMarkTodoIncompleteAsync(Guid id);
}
