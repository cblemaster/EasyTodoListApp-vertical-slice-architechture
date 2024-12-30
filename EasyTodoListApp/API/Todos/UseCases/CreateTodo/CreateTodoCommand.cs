
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public class CreateTodoCommand : IRequest<CreateTodoResponse>
{
   public string Description { get; set; } = string.Empty;
   public DateOnly? DueDate { get; set; }
   public bool IsImportant { get; set; }
   public bool IsComplete { get; set; }
}
