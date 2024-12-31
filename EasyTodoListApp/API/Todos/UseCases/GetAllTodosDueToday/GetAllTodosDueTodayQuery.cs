
using MediatR;

namespace EasyTodoListApp.API.Todos.UseCases.GetAllTodosDueToday;

public record GetAllTodosDueTodayQuery() : IRequest<GetAllTodosDueTodayResponse> { }
