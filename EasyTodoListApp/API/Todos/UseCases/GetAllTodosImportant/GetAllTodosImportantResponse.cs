
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant;

public record GetAllTodosImportantResponse(IReadOnlyCollection<Todo> AllTodosImportant);
