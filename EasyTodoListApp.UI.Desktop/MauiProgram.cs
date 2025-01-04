
using CommunityToolkit.Maui;
using EasyTodoListApp.UI.Desktop.PageModels;
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services
                .AddTransient<IDataService, HttpDataService>()
                .AddTransient<AllTodosCompletePageModel>()
                .AddTransient<AllTodosDueTodayPageModel>()
                .AddTransient<AllTodosImportantPageModel>()
                .AddTransient<AllTodosNotCompletePageModel>()
                .AddTransient<AllTodosOverduePageModel>();
        return builder.Build();
    }
}
