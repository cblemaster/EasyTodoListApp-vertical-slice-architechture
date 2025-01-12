
using EasyTodoListApp.Desktop.Controls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EasyTodoListApp.Desktop;

public partial class App : Application
{
    public App()
    {
        ServiceCollection serviceCollection = new();
        serviceCollection.AddSingleton<TodoListControlModel>();
        serviceCollection.BuildServiceProvider();
    }
}
