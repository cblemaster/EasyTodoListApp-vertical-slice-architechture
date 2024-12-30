
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant;

public class GetAllTodosImportantHandler : IRequestHandler<GetAllTodosImportantQuery, GetAllTodosImportantResponse>
{
    public Task<GetAllTodosImportantResponse> Handle(GetAllTodosImportantQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
