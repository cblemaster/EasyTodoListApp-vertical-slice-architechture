
using EasyTodoListApp.Domain;
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public record CreateTodoCommand(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete, Todo? NewTodo) : IRequest<CreateTodoResponse>;
