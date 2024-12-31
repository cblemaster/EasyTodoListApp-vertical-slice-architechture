
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;

public class ToggleTodoCompletionHandler(ITodoRepository todoRepository) : IRequestHandler<ToggleTodoCompletionCommand, ToggleTodoCompletionResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<ToggleTodoCompletionResponse> Handle(ToggleTodoCompletionCommand request, CancellationToken cancellationToken)
    {
        Todo? updateTodo = await _todoRepository.GetTodoByIdOrNullAsync(request.Id);
        if (updateTodo is null)
        {
            return new ToggleTodoCompletionResponse($"Todo with id {request.Id.Value} not found!");
        }
        else
        {
            await _todoRepository.ToggleTodoCompletionAsync(request);
            return new ToggleTodoCompletionResponse($"Todo with id {request.Id.Value} updated successfully!");
        }
    }
}
