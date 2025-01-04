
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class AllTodosOverduePage : ContentPage
{
    public AllTodosOverduePage(AllTodosOverduePageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
