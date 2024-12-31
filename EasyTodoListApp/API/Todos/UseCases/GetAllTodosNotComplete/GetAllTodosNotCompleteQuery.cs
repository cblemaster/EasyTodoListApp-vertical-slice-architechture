
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete;

public record GetAllTodosNotCompleteQuery() : IRequest<GetAllTodosNotCompleteResponse> { }
