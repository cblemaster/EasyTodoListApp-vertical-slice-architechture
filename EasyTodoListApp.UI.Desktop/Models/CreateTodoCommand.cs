
namespace EasyTodoListApp.UI.Desktop.Models;

public record CreateTodoCommand(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete);
