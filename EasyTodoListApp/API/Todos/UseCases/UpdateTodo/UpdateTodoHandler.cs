using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, UpdateTodoResponse>
{
    public Task<UpdateTodoResponse> Handle(UpdateTodoCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
