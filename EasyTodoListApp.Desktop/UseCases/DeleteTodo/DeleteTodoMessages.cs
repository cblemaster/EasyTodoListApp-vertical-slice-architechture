using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.DeleteTodo;

public static class DeleteTodoMessages
{
    private const string CAPTION = "Delete todo";
    private static readonly MessageBoxButton _button = MessageBoxButton.OK;
    private static readonly MessageBoxButton _confirmButton = MessageBoxButton.YesNo;

    public static MessageBoxResult ShowTodoToDeleteNotFoundMessage()
    {
        string message = "Could not find to do to delete!";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowConfirmDeleteTodoMessage(string description)
    {
        string message = $"Are you sure you want to delete {description}? This operation is permanent!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _confirmButton, icon);
    }

    public static MessageBoxResult ShowDeleteTodoSucceededMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowDeleteTodoFailedMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowDeleteTodoErrorMessage(string error)
    {
        string message = $"Delete todo failed with this error: {error}";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCannotDeleteImportantTodoMessage()
    {
        string message = "Important todos cannot be deleted!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }
}
