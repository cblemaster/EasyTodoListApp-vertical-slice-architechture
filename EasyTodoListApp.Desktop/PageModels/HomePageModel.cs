﻿
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using EasyTodoListApp.Desktop.Handlers;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.PageModels;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EasyTodoListApp.Desktop.Pages;

public partial class HomePageModel : ObservableObject
{
    private readonly IDataService _dataService;
    private readonly IUIHandlers _uiHandlers;

    public HomePageModel(IDataService dataService, IUIHandlers uiHandlers)
    {
        _dataService = dataService;
        _uiHandlers = uiHandlers;

        WeakReferenceMessenger.Default.Register<TodosChangedMessage>(this, async (recipeint, message) =>
            await LoadDataForSelectedTabAsync());
    }

    [ObservableProperty]
    public ReadOnlyCollection<TodoDTO> _todos = new([]);

    [ObservableProperty]
    public TabItem _selectedTabItem = default!;

    [RelayCommand]
    public async Task SelectedTabChangedAsync() => await LoadDataForSelectedTabAsync();

    private async Task LoadDataForSelectedTabAsync()
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
                ReadOnlyCollection<TodoDTO> todoDTOs = (await _dataService.GetAllTodosCompleteAsync()).ToList().AsReadOnly();
                Todos = todoDTOs;
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
