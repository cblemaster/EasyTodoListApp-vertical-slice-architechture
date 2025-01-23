
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace EasyTodoListApp.Desktop.Messages;

public class TodosChangedMessage(string message) : ValueChangedMessage<string>(message)
{
}
