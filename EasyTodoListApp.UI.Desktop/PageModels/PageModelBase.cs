
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.UI.Desktop.Models;
using EasyTodoListApp.UI.Desktop.Pages;
using EasyTodoListApp.UI.Desktop.Services;

namespace EasyTodoListApp.UI.Desktop.PageModels;

public abstract partial class PageModelBase(IDataService dataService) : ObservableObject
{
    protected readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public partial IReadOnlyCollection<TodoDTO> Todos { get => field; set; } = [];

    [RelayCommand]
    public void PageAppearing() => LoadDataAsync();
    [RelayCommand]
    public static async Task CreateTodoAsync() => await Shell.Current.Navigation.PushModalAsync(new CreateTodoModalPage());
    [RelayCommand]
    public static async Task DeleteTodoAsync() { }
    [RelayCommand]
    public static async Task UpdateTodoAsync() { }

    public abstract string About { get; }
    public abstract void LoadDataAsync();
}
