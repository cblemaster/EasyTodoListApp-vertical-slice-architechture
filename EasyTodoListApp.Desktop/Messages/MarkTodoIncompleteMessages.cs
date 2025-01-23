
using System.Windows;

namespace EasyTodoListApp.Desktop.Messages;

public static class MarkTodoIncompleteMessages
{
    private const string CAPTION = "Mark todo incomplete";
    private static readonly MessageBoxButton _button = MessageBoxButton.OK;

    public static MessageBoxResult ShowMarkTodoIncompleteSucceededMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowMarkTodoIncompleteFailedMessage(string message)
    {
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowMarkTodoIncompleteErrorMessage(string error)
    {
        string message = $"Create todo failed with this error: {error}";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowTodoToMarkIncompleteNotFoundMessage()
    {
        string message = "Could not find to do to mark incomplete!";
        MessageBoxImage icon = MessageBoxImage.Error;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }

    public static MessageBoxResult ShowCannotMarkIncompleteANotCompletedTodoMessage()
    {
        string message = "Todos that are not complete cannot be marked incomplete!";
        MessageBoxImage icon = MessageBoxImage.Information;
        return MessageBox.Show(message, CAPTION, _button, icon);
    }
}
