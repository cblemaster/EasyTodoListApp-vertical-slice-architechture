
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;
using System.Runtime.CompilerServices;

namespace EasyTodoListApp.API.Todos.UseCases.DeleteTodo;

public class DeleteTodoHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        Todo? todo = await _todoRepository.GetTodoByIdOrNullAsync(request.Id);
        if (todo is null)
        {
            return new DeleteTodoResponse($"Todo with id { request.Id.Value } not found!");
        }
        else if (todo.IsImportant && !todo.IsComplete)
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
