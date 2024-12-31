using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete
{
    public class GetAllTodosNotCompleteQuery : IRequest<GetAllTodosNotCompleteResponse> { }
}
