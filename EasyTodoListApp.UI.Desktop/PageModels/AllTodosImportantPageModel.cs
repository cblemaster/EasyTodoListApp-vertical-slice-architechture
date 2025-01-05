
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosImportantPageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override string About => "This view shows all todos marked as important. The todos are sorted by due date descending,\nthen alphabetically by description. Todos that are complete are excluded from this view,\nto keep the view focused. To see todos that are marked as complete, navigate to 'Complete'.";

    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosImportantAsync()).ToList().AsReadOnly();
}
