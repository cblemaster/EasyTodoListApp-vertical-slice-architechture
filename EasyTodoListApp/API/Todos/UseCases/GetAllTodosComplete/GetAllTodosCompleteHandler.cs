
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosComplete;

public class GetAllTodosCompleteHandler : IRequestHandler<GetAllTodosCompleteQuery, GetAllTodosCompleteResponse>
{
    public Task<GetAllTodosCompleteResponse> Handle(GetAllTodosCompleteQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
