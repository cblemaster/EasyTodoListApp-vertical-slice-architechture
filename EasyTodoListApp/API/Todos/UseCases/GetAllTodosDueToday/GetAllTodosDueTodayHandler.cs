
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosDueToday;

public class GetAllTodosDueTodayHandler : IRequestHandler<GetAllTodosDueTodayQuery, GetAllTodosDueTodayResponse>
{
    public Task<GetAllTodosDueTodayResponse> Handle(GetAllTodosDueTodayQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
