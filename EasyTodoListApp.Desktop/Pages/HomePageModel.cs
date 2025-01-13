
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services;
using System.Collections.ObjectModel;

namespace EasyTodoListApp.Desktop.Pages;

public partial class HomePageModel(IDataService dataService) : ObservableObject
{
    private readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public ReadOnlyCollection<TodoDTO> _todos = new([]);

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        IEnumerable<TodoDTO> response = await _dataService.GetAllTodosNotCompleteAsync();
        Todos = response.ToList().AsReadOnly();
    }
}
