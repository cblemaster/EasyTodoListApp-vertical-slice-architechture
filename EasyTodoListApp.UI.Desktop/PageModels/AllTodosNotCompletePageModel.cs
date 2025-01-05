
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosNotCompletePageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override string About => "This view shows all todos not marked as complete. The todos are sorted by due date descending,\nthen alphabetically by description. To see todos that are marked as complete, navigate to 'Complete'.";

    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosNotCompleteAsync()).ToList().AsReadOnly();
}
