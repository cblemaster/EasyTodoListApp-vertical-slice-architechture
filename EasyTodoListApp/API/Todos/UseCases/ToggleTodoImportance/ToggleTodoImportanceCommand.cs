
using EasyTodoListApp.Domain;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;

public record ToggleTodoImportanceCommand(Identifier<Todo> Id) : IRequest<ToggleTodoImportanceResponse>;
