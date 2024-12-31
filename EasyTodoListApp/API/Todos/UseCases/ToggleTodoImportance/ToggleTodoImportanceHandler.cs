using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;

public class ToggleTodoImportanceHandler : IRequestHandler<ToggleTodoImportanceCommand, ToggleTodoImportanceResponse>
{
    public Task<ToggleTodoImportanceResponse> Handle(ToggleTodoImportanceCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
