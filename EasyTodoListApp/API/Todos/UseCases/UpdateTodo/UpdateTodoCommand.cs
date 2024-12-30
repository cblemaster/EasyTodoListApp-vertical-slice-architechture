
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.UpdateTodo;

public class UpdateTodoCommand : IRequest<UpdateTodoResponse>
{
   public string Description { get; set; } = string.Empty;
   public DateOnly? DueDate { get; set; }
   public Guid Identifier { get; set; }
}
