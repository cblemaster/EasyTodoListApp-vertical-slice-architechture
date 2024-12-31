
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetTodoById;

public class GetTodoByIdHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodoByIdQuery, GetTodoByIdResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<GetTodoByIdResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        Todo? todo = await _todoRepository.GetTodoByIdOrNullAsync(request.Id);
        return todo is null ? new GetTodoByIdResponse(TodoNotFound) : new GetTodoByIdResponse(todo);
    }
}
