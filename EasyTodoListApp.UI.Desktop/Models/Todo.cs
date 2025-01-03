
namespace EasyTodoListApp.UI.Desktop.Models;

internal record Todo(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete, DateTime CreateDate, DateTime? UpdateDate, Guid Id);
