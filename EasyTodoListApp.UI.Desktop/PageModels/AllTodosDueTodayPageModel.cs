
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class AllTodosDueTodayPageModel(IDataService dataService) : PageModelBase(dataService)
{
    public override string About => "This view shows all todos having a due date that is today. The todos are sorted alphabetically by description.\nTodos that are complete are excluded from this view, to keep the view focused. To see todos that are complete,\nnavigate to 'Complete'.";

    public override async void LoadDataAsync() => Todos = (await _dataService.GetAllTodosDueTodayAsync()).ToList().AsReadOnly();
}
