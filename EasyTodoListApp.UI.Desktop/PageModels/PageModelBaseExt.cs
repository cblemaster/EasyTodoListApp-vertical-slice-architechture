
using EasyTodoListApp.UI.Desktop.Models;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class PageModelBase
{
    public partial IReadOnlyCollection<TodoDTO> Todos { get; set; }
}
