
namespace EasyTodoListApp.Desktop.Models;

public record UpdateTodoDTO(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete, Guid Id);
