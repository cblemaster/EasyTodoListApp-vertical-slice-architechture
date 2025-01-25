
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.UseCases.CreateTodo;
using EasyTodoListApp.Desktop.UseCases.DeleteTodo;
using EasyTodoListApp.Desktop.UseCases.GetTodos;
using EasyTodoListApp.Desktop.UseCases.MarkTodoIncomplete;
using EasyTodoListApp.Desktop.UseCases.UpdateTodo;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EasyTodoListApp.Desktop;

public partial class App : Application
{
    public IServiceProvider Services { get; }

    public App()
    {
        Services = ConfigureServices();
        InitializeComponent();
    }

    public new static App Current => (App)Application.Current;

    private static ServiceProvider ConfigureServices()
    {
        ServiceCollection services = new();
        return services
            .AddSingleton<IDataService, HttpDataService>()
            .AddSingleton<ICreateTodoCommandProcessor, CreateTodoCommandProcessor>()
            .AddSingleton<IDeleteTodoCommandProcessor, DeleteTodoCommandProcessor>()
            .AddSingleton<IUpdateTodoCommandProcessor, UpdateTodoCommandProcessor>()
            .AddSingleton<IMarkTodoIncompleteCommandProcessor, MarkTodoIncompleteCommandProcessor>()
            .AddSingleton<GetTodosPageModel>()
            .AddTransient<CreateTodoPageModel>()
            .AddTransient<UpdateTodoPageModel>()
            .BuildServiceProvider();
    }
}
