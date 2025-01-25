namespace EasyTodoListApp.Desktop.Interfaces;

public interface IMarkTodoIncompleteCommandProcessor
{
    Task TryMarkTodoIncompleteAsync(Guid id);
}
