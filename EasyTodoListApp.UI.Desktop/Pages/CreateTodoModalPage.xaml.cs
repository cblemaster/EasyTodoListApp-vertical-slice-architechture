
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class CreateTodoModalPage : ContentPage
{
    public CreateTodoModalPage()
    {
        InitializeComponent();

        Shell shell = Shell.Current;

        IViewHandler? handler = shell.Handler;
        if (handler is null) { return; }

        IMauiContext? context = handler.MauiContext;
        if (context is null) { return; }

        IServiceProvider services = context.Services;

        CreateTodoPageModel? pageModel = services.GetService<CreateTodoPageModel>();
        if (pageModel is null) { return; }

        BindingContext = pageModel;
    }
}
