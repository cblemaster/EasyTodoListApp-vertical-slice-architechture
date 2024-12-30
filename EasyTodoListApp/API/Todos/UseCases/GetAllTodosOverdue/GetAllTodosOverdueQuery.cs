using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue
{
   public class GetAllTodosOverdueQuery : IRequest<GetAllTodosOverdueResponse> { }
}
