
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue;

public record GetAllTodosOverdueResponse(IReadOnlyCollection<Todo> AllTodosOverdue);
