﻿
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoCompletion;
using EasyTodoListApp.API.Todos.UseCases.ToggleTodoImportance;
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.Domain;
using EasyTodoListApp.Infrastructure.DatabaseContext;

namespace EasyTodoListApp.Infrastructure.Repository;

public class TodoRepository(EasyTodoListAppDbContext context) : ITodoRepository
{
    private readonly EasyTodoListAppDbContext _context = context;

    #region commands
    public async Task CreateTodoAsync(CreateTodoCommand command)
    {
        _context.Set<Todo>().Add(command.NewTodo);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteTodoAsync(Identifier<Todo> id)
    {
        Todo? entity = await GetTodoByIdOrNullAsync(id);
        if (entity is not null)
        {
            _context.Set<Todo>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    public async Task ToggleTodoImportanceAsync(ToggleTodoImportanceCommand command)
    {
        Todo entity = await GetTodoByIdOrNullAsync(command.Id);
        entity.SetIsImportant(!entity.IsImportant);
        await _context.SaveChangesAsync();
    }

    public async Task ToggleTodoCompletionAsync(ToggleTodoCompletionCommand command)
    {
        Todo entity = await GetTodoByIdOrNullAsync(command.Id);
        entity.SetIsComplete(!entity.IsComplete);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateTodoAsync(UpdateTodoCommand command)
    {
        Todo entity = await GetTodoByIdOrNullAsync(command.Id);
        entity.SetDescription(command.Description);
        entity.SetDueDate(command.DueDate);
        entity.SetUpdateDate();
        await _context.SaveChangesAsync();
    }
    #endregion commands

    #region queries


    public IEnumerable<Todo> GetAllTodosComplete() => _context.Set<Todo>().Where(t => t.IsComplete);
    public IEnumerable<Todo> GetAllTodosNotComplete() => _context.Set<Todo>().Where(t => !t.IsComplete);
    public async Task<Todo?> GetTodoByIdOrNullAsync(Identifier<Todo> id) => await _context.Set<Todo>().FindAsync(id.Value);
    #endregion queries
}
