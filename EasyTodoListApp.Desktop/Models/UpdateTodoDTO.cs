
namespace EasyTodoListApp.Desktop.Models;

public record UpdateTodoDTO(string Description, DateOnly? DueDate, Guid Id);
