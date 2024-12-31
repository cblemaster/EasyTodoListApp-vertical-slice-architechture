using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant;

public class GetAllTodosImportantHandler(ITodoRepository todoRepository) : IRequestHandler<GetAllTodosImportantQuery, GetAllTodosImportantResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<GetAllTodosImportantResponse> Handle(GetAllTodosImportantQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Todo> todos =
            _todoRepository
                .GetAllTodosNotComplete()
                .Where(t => t.IsImportant)
                .OrderByDescending(d => d.DueDate)
                .ThenBy(d => d.Description.Value, StringComparer.CurrentCultureIgnoreCase)
                .ToList()
                .AsReadOnly();
        return new GetAllTodosImportantResponse(todos);
    }
}
