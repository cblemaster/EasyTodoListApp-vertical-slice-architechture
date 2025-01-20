
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.DeleteTodo;

public class DeleteTodoHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        Todo? deleteTodo = await _todoRepository.GetTodoByIdOrNullAsync(request.Id);
        if (deleteTodo is null)
        {
            return new DeleteTodoResponse($"Todo with id {request.Id.Value} not found!");
        }
        else if (deleteTodo.IsImportant && !deleteTodo.IsComplete)
        {
            return new DeleteTodoResponse("Important todos that are not complete cannot be deleted!");
        }
        else
        {
            await _todoRepository.DeleteTodoAsync(request.Id);
            return new DeleteTodoResponse($"Todo with id {request.Id.Value} deleted successfully!");
        }
    }
}
