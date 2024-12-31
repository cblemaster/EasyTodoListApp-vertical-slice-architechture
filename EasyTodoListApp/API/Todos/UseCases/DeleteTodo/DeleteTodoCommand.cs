
using EasyTodoListApp.Domain;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.DeleteTodo;

public record DeleteTodoCommand(Identifier<Todo> Id) : IRequest<DeleteTodoResponse>;
