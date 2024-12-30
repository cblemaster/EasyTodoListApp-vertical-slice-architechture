
namespace EasyTodoListApp.API.Todos.UseCases.UpdateTodo;

public class UpdateTodoCommand
{
   public string Description { get; set; } = string.Empty;
   public DateOnly? DueDate { get; set; }
   public Guid Identifier { get; set; }
}
