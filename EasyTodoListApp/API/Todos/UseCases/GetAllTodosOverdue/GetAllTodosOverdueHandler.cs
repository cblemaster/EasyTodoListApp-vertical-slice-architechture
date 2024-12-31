
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue;

public class GetAllTodosOverdueHandler(ITodoRepository todoRepository) : IRequestHandler<GetAllTodosOverdueQuery, GetAllTodosOverdueResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<GetAllTodosOverdueResponse> Handle(GetAllTodosOverdueQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Todo> todos =
            _todoRepository
                .GetAllTodosNotComplete()
                .Where(t => t.DueDate.HasValue && t.DueDate.Value < DateOnly.FromDateTime(DateTime.Today))
                .OrderByDescending(d => d.DueDate)
                .ThenBy(d => d.Description.Value, StringComparer.CurrentCultureIgnoreCase)
                .ToList()
                .AsReadOnly();
        return new GetAllTodosOverdueResponse(todos);
    }
}
