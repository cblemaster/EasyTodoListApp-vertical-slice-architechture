
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public class CreateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        Todo createTodo = Todo.Create(request.Description, request.DueDate, request.IsImportant, request.IsComplete);
        request = request with { NewTodo = createTodo };
        await _todoRepository.CreateTodoAsync(request);
        return new CreateTodoResponse(createTodo, $"/todos/{createTodo.Identifier.Value}");
    }
}
