
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosCompletePageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override async void LoadData() => Todos = (await _dataService.GetAllTodosCompleteAsync()).ToList().AsReadOnly();
}
