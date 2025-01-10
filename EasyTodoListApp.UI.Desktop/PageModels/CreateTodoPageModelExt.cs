
using EasyTodoListApp.UI.Desktop.Services.Validation;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public partial class CreateTodoPageModel
{
    public partial ValidatableObject<string> Description { get; set; }
    public partial DateOnly? DueDate { get; set; }
    public partial bool IsImportant { get; set; }
    public partial bool IsComplete { get; set; }
    public partial bool IsValid { get; set; }
}
