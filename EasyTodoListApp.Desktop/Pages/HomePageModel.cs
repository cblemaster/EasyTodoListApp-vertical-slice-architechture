
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.Pages;

public partial class HomePageModel(IDataService dataService) : ObservableObject
{
    private readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public ReadOnlyCollection<TodoDTO> _todos = new([]);

    [ObservableProperty]
    public TabItem _selectedTabItem = default!;

    [RelayCommand]
    public async Task SelectedTabChangedAsync()
    {
        switch (SelectedTabItem.Header)
        {
            case "Not complete":
                Todos = (await _dataService.GetAllTodosNotCompleteAsync()).ToList().AsReadOnly();
                break;
            case "Due today":
                Todos = (await _dataService.GetAllTodosDueTodayAsync()).ToList().AsReadOnly();
                break;
            case "Overdue":
                Todos = (await _dataService.GetAllTodosOverdueAsync()).ToList().AsReadOnly();
                break;
            case "Important":
                Todos = (await _dataService.GetAllTodosImportantAsync()).ToList().AsReadOnly();
                break;
            case "Complete":
                Todos = (await _dataService.GetAllTodosCompleteAsync()).ToList().AsReadOnly();
                break;
            default:
                break;
        }
    }
}
