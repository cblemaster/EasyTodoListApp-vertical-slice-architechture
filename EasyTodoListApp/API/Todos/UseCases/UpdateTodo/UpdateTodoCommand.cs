using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.UpdateTodo;

public record UpdateTodoCommand(string Description, DateOnly? DueDate, bool IsImportant, bool IsComplete, Guid Id) : IRequest<UpdateTodoResponse>;
