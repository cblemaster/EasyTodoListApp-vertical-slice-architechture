
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Handlers;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.PageModels;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.Pages;

public partial class HomePageModel(IDataService dataService, IUIHandlers uiHandlers) : ObservableObject
{
    private readonly IDataService _dataService = dataService;
    private readonly IUIHandlers _uiHandlers = uiHandlers;

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
        }
    }

    [RelayCommand]
    public static void Create()
    {
        CreateTodoWindow ctw = new();
        ctw.ShowDialog();
    }

    [RelayCommand]
    public async Task DeleteAsync(Guid id) => await _uiHandlers.TryHandleDeleteTodoAsync(id);

    [RelayCommand]
    public static async Task UpdateAsync(Guid id)
    {
        UpdateTodoWindow utw = new();
        if (utw.Content is UpdateTodoPage updatePage)
        {
            UpdateTodoPageModel pageModel = (UpdateTodoPageModel)updatePage.DataContext;
            await pageModel.LoadTodoAsync(id);
            utw.ShowDialog();
        }
    }

    [RelayCommand]
    public async Task MarkIncompleteAsync(TodoDTO dto) => await _uiHandlers.TryMarkTodoIncompleteAsync(dto.Id);
}
