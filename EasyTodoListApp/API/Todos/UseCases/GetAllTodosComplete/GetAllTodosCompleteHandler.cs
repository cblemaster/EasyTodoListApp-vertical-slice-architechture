using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosComplete;

public class GetAllTodosCompleteHandler(ITodoRepository todoRepository) : IRequestHandler<GetAllTodosCompleteQuery, GetAllTodosCompleteResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<GetAllTodosCompleteResponse> Handle(GetAllTodosCompleteQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Todo> todos =
            _todoRepository
                .GetAllTodosComplete()
                .OrderByDescending(d => d.DueDate)
                .ThenBy(d => d.Description.Value, StringComparer.CurrentCultureIgnoreCase)
                .ToList()
                .AsReadOnly();
        return new GetAllTodosCompleteResponse(todos);
    }
}
