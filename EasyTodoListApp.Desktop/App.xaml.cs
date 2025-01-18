
using EasyTodoListApp.Desktop.PageModels;
using EasyTodoListApp.Desktop.Pages;
using EasyTodoListApp.Desktop.Services;
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
            .AddSingleton<HomePageModel>()
            .AddTransient<CreateTodoPageModel>()
            .BuildServiceProvider();
    }
}
