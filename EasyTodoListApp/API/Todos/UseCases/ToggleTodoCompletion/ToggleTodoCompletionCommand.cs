using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion
{
   public class ToggleTodoCompletionCommand : IRequest<ToggleTodoCompletionResponse>
    {
      public Guid Identifier { get; set; }
   }
}
