
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.UI.Desktop.Models;
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public abstract partial class PageModelBase(IDataService dataService) : ObservableObject
{
    protected readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public IReadOnlyCollection<TodoDTO> _todos = default!;

    [RelayCommand]
    public void PageAppearingAsync() => LoadDataAsync();

    public abstract void LoadDataAsync();
}
