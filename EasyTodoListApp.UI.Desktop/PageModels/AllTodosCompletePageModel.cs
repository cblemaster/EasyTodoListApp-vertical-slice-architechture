
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosCompletePageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override string About => "This view shows all todos marked as complete. The todos are sorted by due date descending,\nthen alphabetically by description. To see todos that are not marked as complete, navigate to\n'Not complete'.";

    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosCompleteAsync()).ToList().AsReadOnly();
}
