using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.MarkTodoIncomplete;

public class MarkTodoIncompleteHandler(ITodoRepository todoRepository) : IRequestHandler<MarkTodoIncompleteCommand, MarkTodoIncompleteResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<MarkTodoIncompleteResponse> Handle(MarkTodoIncompleteCommand request, CancellationToken cancellationToken)
    {
        Todo? updateTodo = await _todoRepository.GetTodoByIdOrNullAsync(Identifier<Todo>.Create(request.Id));
        if (updateTodo is null)
        {
            return new MarkTodoIncompleteResponse($"Todo with id {request.Id} not found!");
        }
        else if (!updateTodo.IsComplete)
        {
            return new MarkTodoIncompleteResponse("Todos that are not complete cannot be marked incomplete!");
        }
        else
        {
            await _todoRepository.MarkTodoIncompleteAsync(request);
            return new MarkTodoIncompleteResponse($"Todo with id {request.Id} successfully marked incomplete!");
        }
    }
}
