
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosComplete;

public record GetAllTodosCompleteResponse(IReadOnlyCollection<Todo> AllTodosComplete);
