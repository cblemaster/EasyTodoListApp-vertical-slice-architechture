using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance
{
   public class ToggleTodoImportanceCommand : IRequest<ToggleTodoImportanceResponse>
    {
      public Guid Identifier { get; set; }
   }
}
