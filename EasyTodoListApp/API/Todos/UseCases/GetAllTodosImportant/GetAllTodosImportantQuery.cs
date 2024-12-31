
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant;

public record GetAllTodosImportantQuery() : IRequest<GetAllTodosImportantResponse> { }
