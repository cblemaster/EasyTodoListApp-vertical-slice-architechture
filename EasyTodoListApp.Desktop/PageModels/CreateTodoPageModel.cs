
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.Services.Responses;
using System.Net.Http;

namespace EasyTodoListApp.Desktop.PageModels;

public partial class CreateTodoPageModel(IDataService dataService) : ObservableObject
{
    private readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateTime _dueDate;

    [ObservableProperty]
    public bool _hasDueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [RelayCommand]
    public async Task SaveAsync()
    {
        // TODO: Validation
        CreateTodoDTO dto = new
            (Description,
            HasDueDate ? (DueDate == DateTime.MinValue ? null : DateOnly.FromDateTime(DueDate)) : null,
            IsImportant,
            IsComplete);

        string message = string.Empty;

        try
        {
            DataServiceResponse<string> createResponse = await _dataService.TryCreateTodoAsync(dto);
            switch (createResponse.ResponseType)
            {
                case ResponseType.Success:
                case ResponseType.Failure:
                    //TODO...
                    message = createResponse.Messgage;
                    break;
                case ResponseType.NotSet:
                default:
                    break;
            }
        }
        catch (HttpRequestException ex)
        {
            //TODO...
            message = $"Create todo failed, the server response was status {ex.StatusCode}";
        }
    }
}
