
using EasyTodoListApp.Desktop.PageModels;
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.Pages;

public partial class UpdateTodoPage : Page
{
    public UpdateTodoPage()
    {
        InitializeComponent();
        object? foundService = App.Current.Services.GetService(typeof(UpdateTodoPageModel));
        if (foundService is UpdateTodoPageModel pageModel)
        {
            DataContext = pageModel;
        }
    }
}
