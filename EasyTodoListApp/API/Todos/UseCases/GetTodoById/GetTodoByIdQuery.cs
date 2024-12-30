using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetTodoById
{
   public class GetTodoByIdQuery : IRequest<GetTodoByIdResponse>
    {
      public Guid Identifier { get; set; }
   }
}
