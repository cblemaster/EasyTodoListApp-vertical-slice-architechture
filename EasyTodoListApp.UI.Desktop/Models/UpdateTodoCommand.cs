
namespace EasyTodoListApp.UI.Desktop.Models;

internal record UpdateTodoCommand(string Description, DateOnly? DueDate, Guid Id);
