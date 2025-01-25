
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.UseCases.CreateTodo;

public partial class CreateTodoPage : Page
{
    public CreateTodoPage()
    {
        InitializeComponent();
        object? foundService = App.Current.Services.GetService(typeof(CreateTodoPageModel));
        if (foundService is CreateTodoPageModel pageModel)
        {
            DataContext = pageModel;
        }
    }
}
