
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.UseCases.UpdateTodo;

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
