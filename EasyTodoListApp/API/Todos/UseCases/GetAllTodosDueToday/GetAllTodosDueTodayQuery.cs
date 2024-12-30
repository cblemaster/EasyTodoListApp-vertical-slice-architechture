using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosDueToday
{
   public class GetAllTodosDueTodayQuery : IRequest<GetAllTodosDueTodayResponse> { }
}
