
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;

public class ToggleTodoImportanceHandler(ITodoRepository todoRepository) : IRequestHandler<ToggleTodoImportanceCommand, ToggleTodoImportanceResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<ToggleTodoImportanceResponse> Handle(ToggleTodoImportanceCommand request, CancellationToken cancellationToken)
    {
        Todo? updateTodo = await _todoRepository.GetTodoByIdOrNullAsync(request.Id);
        if (updateTodo is null)
        {
            return new ToggleTodoImportanceResponse($"Todo with id {request.Id.Value} not found!");
        }
        else if (updateTodo.IsComplete)
        {
            return new ToggleTodoImportanceResponse("Todos that are complete cannot be updated!");
        }
        else
        {
            await _todoRepository.ToggleTodoImportanceAsync(request);
            return new ToggleTodoImportanceResponse($"Todo with id {request.Id.Value} updated successfully!");
        }
    }
}
