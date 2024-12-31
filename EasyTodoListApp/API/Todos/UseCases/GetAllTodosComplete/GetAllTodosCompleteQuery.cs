
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosComplete;

public record GetAllTodosCompleteQuery() : IRequest<GetAllTodosCompleteResponse> { }
