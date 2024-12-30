
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
    {
        public Task<CreateTodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
