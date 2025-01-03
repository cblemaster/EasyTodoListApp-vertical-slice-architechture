
namespace EasyTodoListApp.UI.Desktop.Models;

public record UpdateTodoCommand(string Description, DateOnly? DueDate, Guid Id);
