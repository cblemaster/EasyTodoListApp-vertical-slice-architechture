using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete
{
   public class GetAllTodosNotCompleteQuery : IRequest<GetAllTodosNotCompleteResponse> { }
}
