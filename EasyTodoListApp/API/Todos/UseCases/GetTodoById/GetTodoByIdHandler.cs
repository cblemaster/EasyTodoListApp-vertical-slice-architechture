
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetTodoById;

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, GetTodoByIdResponse>
{
    public Task<GetTodoByIdResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
