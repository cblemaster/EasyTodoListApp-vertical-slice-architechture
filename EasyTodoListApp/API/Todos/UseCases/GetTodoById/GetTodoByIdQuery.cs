
using EasyTodoListApp.Domain;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetTodoById;

public record GetTodoByIdQuery(Identifier<Todo> Id) : IRequest<GetTodoByIdResponse>;
