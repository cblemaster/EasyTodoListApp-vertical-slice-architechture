
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class AllTodosCompletePage : ContentPage
{
    public AllTodosCompletePage(AllTodosCompletePageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
