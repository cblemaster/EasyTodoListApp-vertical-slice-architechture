using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.Repository;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete;

public class GetAllTodosNotCompleteHandler(ITodoRepository todoRepository) : IRequestHandler<GetAllTodosNotCompleteQuery, GetAllTodosNotCompleteResponse>
{
    private readonly ITodoRepository _todoRepository = todoRepository;

    public async Task<GetAllTodosNotCompleteResponse> Handle(GetAllTodosNotCompleteQuery request, CancellationToken cancellationToken) =>
        await Task.Run(() =>
        {
            IReadOnlyCollection<Todo> todos =
            _todoRepository
            .GetAllTodosNotComplete()
            .OrderByDescending(d => d.DueDate)
            .ThenBy(d => d.Description.Value, StringComparer.CurrentCultureIgnoreCase)
            .ToList().AsReadOnly();
            return new GetAllTodosNotCompleteResponse(todos);
        });
}
