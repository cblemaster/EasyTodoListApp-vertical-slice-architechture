
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;

public class ToggleTodoCompletionHandler : IRequestHandler<ToggleTodoCompletionCommand, ToggleTodoCompletionResponse>
{
    public Task<ToggleTodoCompletionResponse> Handle(ToggleTodoCompletionCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
