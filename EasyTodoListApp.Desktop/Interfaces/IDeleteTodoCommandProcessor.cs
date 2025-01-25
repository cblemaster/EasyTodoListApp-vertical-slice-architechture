namespace EasyTodoListApp.Desktop.Interfaces;

public interface IDeleteTodoCommandProcessor
{
    Task TryHandleDeleteTodoAsync(Guid id);
}
