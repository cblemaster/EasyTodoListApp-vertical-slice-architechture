
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.Pages;

public partial class HomePage : Page
{
    public HomePage()
    {
        InitializeComponent();
        object? foundService = App.Current.Services.GetService(typeof(HomePageModel));
        if (foundService is HomePageModel pageModel)
        {
            DataContext = pageModel;
            Task.Run(() => pageModel.LoadDataAsync());
        }
    }
}
