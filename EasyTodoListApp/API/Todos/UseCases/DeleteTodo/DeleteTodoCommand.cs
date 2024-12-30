
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.DeleteTodo;

public class DeleteTodoCommand : IRequest<DeleteTodoResponse>
{
   public Guid Identifier { get; set; }
}
