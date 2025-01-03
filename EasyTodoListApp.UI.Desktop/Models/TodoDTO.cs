
namespace EasyTodoListApp.UI.Desktop.Models;

public record TodoDTO(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete, DateTime CreateDate, DateTime? UpdateDate, Guid Id);
