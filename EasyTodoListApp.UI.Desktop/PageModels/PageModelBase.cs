
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
    public void PageAppearing() => LoadDataAsync();
    [RelayCommand]
    public async Task CreateTodoAsync() { }
    [RelayCommand]
    public async Task DeleteTodoAsync() { }
    [RelayCommand]
    public async Task UpdateTodoAsync() { }

    public abstract string About { get; }
    public abstract void LoadDataAsync();
}
