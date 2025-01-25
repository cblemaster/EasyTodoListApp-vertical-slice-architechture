namespace EasyTodoListApp.Desktop.Interfaces;

public interface IUpdateTodoCommandProcessor
{
    Task TryHandleUpdateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete, Guid id);
}
