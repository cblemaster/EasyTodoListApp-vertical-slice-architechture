
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.DeleteTodo;
using EasyTodoListApp.API.Todos.UseCases.GetAllTodosComplete;
using EasyTodoListApp.API.Todos.UseCases.GetAllTodosDueToday;
using EasyTodoListApp.API.Todos.UseCases.GetAllTodosImportant;
using EasyTodoListApp.API.Todos.UseCases.GetAllTodosNotComplete;
using EasyTodoListApp.API.Todos.UseCases.GetAllTodosOverdue;
using EasyTodoListApp.API.Todos.UseCases.GetTodoById;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.API.Todos.Validation;
using EasyTodoListApp.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyTodoListApp.API.Todos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync([FromBody] CreateTodoCommand command)
        {
            (bool IsValid, string ErrorMessage) = ValidateDescription.Validate(command.Description);

            if (!IsValid)
            {
                return BadRequest(ErrorMessage);
            }
            else
            {
                CreateTodoResponse response = await _mediator.Send(command);
                return Created(response.Uri, response.Todo);
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync([FromBody] UpdateTodoCommand command, Guid id)
        {
            (bool IsValid, string ErrorMessage) = ValidateDescription.Validate(command.Description);

            if (!IsValid)
            {
                return BadRequest(ErrorMessage);
            }
            else
            {
                // TODO: The "not found" evaluation here is pretty brittle
                command = command with { Id = command.Id with { Value = id } };
                UpdateTodoResponse response = await _mediator.Send(command);
                return !string.IsNullOrWhiteSpace(response.Result)
                    ? response.Result.Contains("not found") ? NotFound() : BadRequest(response.Result)
                    : NoContent();
            }
        }
        [HttpPut("{id:guid}/importance")]
        public async Task<IActionResult> ToggleTodoImportanceAsync(Guid id)
        {
            ToggleTodoImportanceCommand command = new(Identifier<Todo>.Create(id));
            ToggleTodoImportanceResponse response = await _mediator.Send(command);
            return !string.IsNullOrWhiteSpace(response.Result)
                ? response.Result.Contains("not found") ? NotFound() : BadRequest(response.Result)
                : NoContent();
        }
        [HttpPut("{id:guid}/completion")]
        public async Task<IActionResult> ToggleTodoCompletionAsync(Guid id)
        {
            ToggleTodoCompletionCommand command = new(Identifier<Todo>.Create(id));
            ToggleTodoCompletionResponse response = await _mediator.Send(command);
            return !string.IsNullOrWhiteSpace(response.Result)
                ? response.Result.Contains("not found") ? NotFound() : BadRequest(response.Result)
                : NoContent();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            DeleteTodoCommand command = new(Identifier<Todo>.Create(id));
            DeleteTodoResponse response = await _mediator.Send(command);
            return !string.IsNullOrWhiteSpace(response.Result)
                ? response.Result.Contains("not found") ? NotFound() : BadRequest(response.Result)
                : NoContent();
        }

        [HttpGet("complete")]
        public async Task<IActionResult> GetAllTodosCompleteAsync()
        {
            GetAllTodosCompleteQuery query = new();
            GetAllTodosCompleteResponse response = await _mediator.Send(query);
            return Ok(response.AllTodosComplete);
        }
        [HttpGet("duetoday")]
        public async Task<IActionResult> GetAllTodosDueTodayAsync()
        {
            GetAllTodosDueTodayQuery query = new();
            GetAllTodosDueTodayResponse response = await _mediator.Send(query);
            return Ok(response.AllTodosDueToday);
        }
        [HttpGet("important")]
        public async Task<IActionResult> GetAllTodosImportantAsync()
        {
            GetAllTodosImportantQuery query = new();
            GetAllTodosImportantResponse response = await _mediator.Send(query);
            return Ok(response.AllTodosImportant);
        }
        [HttpGet("notcomplete")]
        public async Task<IActionResult> GetAllTodosNotCompleteAsync()
        {
            GetAllTodosNotCompleteQuery query = new();
            GetAllTodosNotCompleteResponse response = await _mediator.Send(query);
            return Ok(response.AllTodosNotComplete);
        }
        [HttpGet("overdue")]
        public async Task<IActionResult> GetAllTodosOverdueAsync()
        {
            GetAllTodosOverdueQuery query = new();
            GetAllTodosOverdueResponse response = await _mediator.Send(query);
            return Ok(response.AllTodosOverdue);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTodoByIdAsync(Guid id)
        {
            GetTodoByIdQuery query = new(Identifier<Todo>.Create(id));
            GetTodoByIdResponse response = await _mediator.Send(query);
            return response.Todo is not null ? Ok(response.Todo) : NotFound($"Todo with id {id} not found!");
        }
    }
}
