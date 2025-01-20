
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.UpdateTodo;

public class UpdateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand, UpdateTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<UpdateTodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        Todo? updateTodo = await _todoRepository.GetTodoByIdOrNullAsync(Identifier<Todo>.Create(request.Id));
        if (updateTodo is null)
        {
            return new UpdateTodoResponse($"Todo with id {request.Id} not found!");
        }
        else if (updateTodo.IsComplete)
        {
            return new UpdateTodoResponse("Todos that are complete cannot be updated!");
        }
        else
        {
            await _todoRepository.UpdateTodoAsync(request);
            return new UpdateTodoResponse($"Todo with id {request.Id} updated successfully!");
        }
    }
}
