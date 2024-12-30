
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete;

public class GetAllTodosNotCompleteHandler : IRequestHandler<GetAllTodosNotCompleteQuery, GetAllTodosNotCompleteResponse>
{
    public Task<GetAllTodosNotCompleteResponse> Handle(GetAllTodosNotCompleteQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
