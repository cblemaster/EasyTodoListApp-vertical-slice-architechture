
namespace EasyTodoListApp.UI.Desktop.Models;

internal record CreateTodoCommand(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete);
