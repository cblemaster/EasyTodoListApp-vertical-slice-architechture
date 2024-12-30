using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant
{
   public class GetAllTodosImportantQuery : IRequest<GetAllTodosImportantResponse> { }
}
