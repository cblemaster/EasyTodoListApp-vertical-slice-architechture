
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.API.Todos.Validation;
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public class CreateTodoHandler(ITodoRepository todoRepository) : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        (bool IsValid, string ErrorMessage) = ValidateDescription.Validate(request.Description);

        if (!IsValid)
        {
            return new CreateTodoResponse(null, null, $"Validation error: {ErrorMessage}!");
        }

        Todo createTodo = Todo.Create(request.Description, request.DueDate, request.IsImportant, request.IsComplete);
        request = request with { NewTodo = createTodo };
        await _todoRepository.CreateTodoAsync(request);
        return new CreateTodoResponse(createTodo, $"/todos/{ createTodo.Identifier.Value }", "Todo created successfully!");
    }
}
