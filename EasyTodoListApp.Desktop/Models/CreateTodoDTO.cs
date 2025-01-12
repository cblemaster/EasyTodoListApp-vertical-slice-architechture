
namespace EasyTodoListApp.Desktop.Models;

public record CreateTodoDTO(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete);
