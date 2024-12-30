
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue;

public class GetAllTodosOverdueHandler : IRequestHandler<GetAllTodosOverdueQuery, GetAllTodosOverdueResponse>
{
    public Task<GetAllTodosOverdueResponse> Handle(GetAllTodosOverdueQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
