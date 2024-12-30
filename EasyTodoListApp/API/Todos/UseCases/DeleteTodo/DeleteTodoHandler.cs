
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.DeleteTodo;

public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
{
    public Task<DeleteTodoResponse> Handle(DeleteTodoCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
