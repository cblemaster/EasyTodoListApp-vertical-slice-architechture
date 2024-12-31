
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public class CreateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validation of request values, no need for null check
        
        Todo newTodo = Todo.Create(request.Description, request.DueDate, request.IsImportant, request.IsComplete);
        request = request with { NewTodo = newTodo };
        await _todoRepository.CreateTodoAsync(request);
        return new CreateTodoResponse(newTodo, $"/todos/{ newTodo.Identifier.Value }");
    }
}
