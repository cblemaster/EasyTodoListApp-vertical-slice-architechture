
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosOverduePageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override string About => "This view shows all todos having a due date that is before today. The todos are sorted by due date descending,\nthen alphabetically by description. Todos that are complete are excluded from this view, to keep the view focused.\nTo see todos that are complete, navigate to 'Complete'.";

    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosOverdueAsync()).ToList().AsReadOnly();
}
