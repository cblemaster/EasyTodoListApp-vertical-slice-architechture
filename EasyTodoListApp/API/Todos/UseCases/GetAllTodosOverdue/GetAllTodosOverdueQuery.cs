
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue;

public record GetAllTodosOverdueQuery() : IRequest<GetAllTodosOverdueResponse> { }
