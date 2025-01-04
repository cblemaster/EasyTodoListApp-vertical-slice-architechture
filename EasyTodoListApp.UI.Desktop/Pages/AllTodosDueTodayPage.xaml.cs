
using EasyTodoListApp.UI.Desktop.PageModels;

namespace EasyTodoListApp.UI.Desktop.Pages;

public partial class AllTodosDueTodayPage : ContentPage
{
    public AllTodosDueTodayPage(AllTodosDueTodayPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}
