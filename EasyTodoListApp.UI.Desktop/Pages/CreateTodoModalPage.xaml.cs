
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class CreateTodoModalPage : ContentPage
{
    public CreateTodoModalPage(CreateTodoPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
