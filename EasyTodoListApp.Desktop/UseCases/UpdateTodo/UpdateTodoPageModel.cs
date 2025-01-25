
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;

namespace EasyTodoListApp.Desktop.UseCases.UpdateTodo;

public partial class UpdateTodoPageModel(IDataService dataService, IUpdateTodoCommandProcessor commandProcessor) : ObservableObject
{
    private readonly IDataService _dataService = dataService;
    private readonly IUpdateTodoCommandProcessor _commandProcessor = commandProcessor;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateTime? _dueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [ObservableProperty]
    public Guid _id;

    [RelayCommand]
    public async Task SaveAsync() => await _commandProcessor.TryHandleUpdateTodoAsync(Description, DueDate, IsImportant, IsComplete, Id);

    public async Task LoadTodoAsync(Guid id)
    {
        DataServiceResponse<TodoDTO> todoResponse = await _dataService.TryGetTodoByIdOrThrowHttpExAsync(id);
        if (todoResponse.Data is null || todoResponse.Data is not TodoDTO todo) { return; }
        DateTime? dueDateToDisplay = todo.DueDate is null ? null : new DateTime(year: todo.DueDate.Value.Year, month: todo.DueDate.Value.Month, day: todo.DueDate.Value.Day);

        Description = todo.Description;
        DueDate = dueDateToDisplay;
        IsImportant = todo.IsImportant;
        IsComplete = todo.IsComplete;
        Id = todo.Id;
    }
}
