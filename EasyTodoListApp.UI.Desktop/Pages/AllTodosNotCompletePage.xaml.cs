
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class AllTodosNotCompletePage : ContentPage
{
    public AllTodosNotCompletePage(AllTodosNotCompletePageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
