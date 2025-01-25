
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Interfaces;

namespace EasyTodoListApp.Desktop.UseCases.CreateTodo;

public partial class CreateTodoPageModel(ICreateTodoCommandProcessor commandProcessor) : ObservableObject
{
    private readonly ICreateTodoCommandProcessor _commandProcessor = commandProcessor;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateTime? _dueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [RelayCommand]
    public async Task SaveAsync() => await _commandProcessor.TryHandleCreateTodoAsync(Description, DueDate, IsImportant, IsComplete);
}
