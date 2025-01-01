
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.CreateTodo;

public record CreateTodoResponse(Todo? Todo, string? Uri, string Result);
