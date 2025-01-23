
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.MarkTodoIncomplete;

public record MarkTodoIncompleteCommand(Guid Id) : IRequest<MarkTodoIncompleteResponse>;
