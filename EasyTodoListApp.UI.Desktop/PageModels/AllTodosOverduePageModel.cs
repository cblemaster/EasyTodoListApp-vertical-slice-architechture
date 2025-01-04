
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosOverduePageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosOverdueAsync()).ToList().AsReadOnly();
}
