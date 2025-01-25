
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.UseCases.GetTodos;

public partial class GetTodosPage : Page
{
    public GetTodosPage()
    {
        InitializeComponent();
        object? foundService = App.Current.Services.GetService(typeof(GetTodosPageModel));
        if (foundService is GetTodosPageModel pageModel)
        {
            DataContext = pageModel;
        }
    }
}
