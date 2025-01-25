using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.UpdateTodo;

public static class UpdateTodoMessages
{
    private const string CAPTION = "Update todo";
    private static readonly MessageBoxButton _button = MessageBoxButton.OK;

    public static MessageBoxResult ShowUpdateTodoSucceededMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowUpdateTodoFailedMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowUpdateTodoErrorMessage(string error)
    {
        string message = $"Create todo failed with this error: {error}";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowUpdateTodoValidationErrorMessage(string error)
    {
        string message = $"The following validation error(s) occured, and must be resolved before the todo can be updated: {error}";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowTodoToUpdateNotFoundMessage()
    {
        string message = "Could not find to do to update!";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCannotUpdateCompletedTodoMessage()
    {
        string message = "Completed todos cannot be updated!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowMarkTodoIncompleteTodoAlreadyIncompleteMessage()
    {
        string message = "Not complete todos cannot be marked incomplete!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowMarkTodoIncompleteTodoIsNotCompleteMessage()
    {
        string message = "Todo is already not complete!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }
}
