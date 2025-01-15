
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Services;

namespace EasyTodoListApp.Desktop.PageModels;

public partial class CreateTodoPageModel(IDataService dataService) : ObservableObject
{
    private readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateOnly _dueDate;

    [ObservableProperty]
    public bool _hasDueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [RelayCommand]
    public void Save()
    {
        var a = 1;
    }
}
