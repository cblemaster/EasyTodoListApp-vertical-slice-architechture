
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Handlers;

namespace EasyTodoListApp.Desktop.PageModels;

public partial class CreateTodoPageModel(IUIHandlers uiHandlers) : ObservableObject
{
    private readonly IUIHandlers _uiHandlers = uiHandlers;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateTime? _dueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [RelayCommand]
    public async Task SaveAsync() => await _uiHandlers.TryHandleCreateTodoAsync(Description, DueDate, IsImportant, IsComplete);
}
