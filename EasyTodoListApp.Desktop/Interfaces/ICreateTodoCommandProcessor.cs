namespace EasyTodoListApp.Desktop.Interfaces;

public interface ICreateTodoCommandProcessor
{
    Task TryHandleCreateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete);
}
