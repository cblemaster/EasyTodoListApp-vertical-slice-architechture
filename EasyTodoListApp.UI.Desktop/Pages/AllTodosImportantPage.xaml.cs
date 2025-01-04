
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class AllTodosImportantPage : ContentPage
{
    public AllTodosImportantPage(AllTodosImportantPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
