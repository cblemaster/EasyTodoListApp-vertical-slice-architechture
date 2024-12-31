
using EasyTodoListApp.Domain;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;

public record ToggleTodoCompletionCommand(Identifier<Todo> Id) : IRequest<ToggleTodoCompletionResponse>;
