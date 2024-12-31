
using EasyTodoListApp.Domain;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosDueToday;

public record GetAllTodosDueTodayResponse(IReadOnlyCollection<Todo> AllTodosDueToday);
