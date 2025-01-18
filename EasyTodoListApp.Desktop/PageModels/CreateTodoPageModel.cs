
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.Services.Responses;
using EasyTodoListApp.Desktop.Windows;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;

namespace EasyTodoListApp.Desktop.PageModels;

public partial class CreateTodoPageModel(IDataService dataService) : ObservableObject
{
    private readonly IDataService _dataService = dataService;

    [ObservableProperty]
    public string _description = string.Empty;

    [ObservableProperty]
    public DateTime? _dueDate;

    [ObservableProperty]
    public bool _isImportant;

    [ObservableProperty]
    public bool _isComplete;

    [RelayCommand]
    public async Task SaveAsync()
    {
        string message = GetValidationMessageOrEmptyString();

        if (!message.Equals(string.Empty))
        {
            CreateTodoMessages.ShowCreateTodoValidationErrorMessage(message);
            return;
        }

        DateOnly? dueDateForDto = DueDate is null ? null : DateOnly.FromDateTime(DueDate.Value);
        CreateTodoDTO dto = new(Description, dueDateForDto, IsImportant, IsComplete);  // TODO: We could just databind the xaml input controls to properties on the dto rather than instantiating the dto here

        try
        {
            DataServiceResponse<string> createResponse = await _dataService.TryCreateTodoAsync(dto);
            switch (createResponse.ResponseType)
            {
                case ResponseType.Success:
                    message = createResponse.Messgage;
                    CreateTodoMessages.ShowCreateTodoSucceededMessage(message);
                    System.Windows.WindowCollection a = App.Current.Windows;    // TODO: I dont like doing UI stuff like this from the page model
                    foreach (object? w in a)
                    {
                        if (w is Window window)
                        {
                            if (window.GetType() == typeof(CreateTodoWindow))
                            {
                                window.Close();
                                break;
                            }
                        }
                    }
                    return;
                case ResponseType.Failure:
                    message = createResponse.Messgage;
                    CreateTodoMessages.ShowCreateTodoFailedMessage(message);  // TODO: These messages arent very elegant and involve too much UI work in the page model
                    return;
                case ResponseType.NotSet:
                default:
                    break;
            }
        }
        catch (HttpRequestException ex)
        {
            message = $"Create todo failed, the server response was status {ex.StatusCode}";
            CreateTodoMessages.ShowCreateTodoErrorMessage(message);
        }

        string GetValidationMessageOrEmptyString()
        {
            string message = string.Empty;
            
            if (string.IsNullOrEmpty(Description))
            {
                message = "Description is required!";
            }
            else if (Regex.Match(Description, @"^\s +$").Success)
            {
                message = "Description cannot be only whitespace characters!";
            }
            else if (Description.Length > 100)
            {
                message = "Description must be 100 or fewer characters!";
            }

            return message;
        }
    }
}
