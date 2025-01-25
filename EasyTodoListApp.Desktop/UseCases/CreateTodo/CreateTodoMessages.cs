using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.CreateTodo;

public static class CreateTodoMessages
{
    private const string CAPTION = "Create todo";
    private static readonly MessageBoxButton _button = MessageBoxButton.OK;

    public static MessageBoxResult ShowCreateTodoSucceededMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCreateTodoFailedMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCreateTodoErrorMessage(string error)
    {
        string message = $"Create todo failed with this error: {error}";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCreateTodoValidationErrorMessage(string error)
    {
        string message = $"The following validation error(s) occured, and must be resolved before the todo can be created: {error}";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }
}
