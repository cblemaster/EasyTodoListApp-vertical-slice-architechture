
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete;

public record GetAllTodosNotCompleteResponse(IReadOnlyCollection<Todo> AllTodosNotComplete);
