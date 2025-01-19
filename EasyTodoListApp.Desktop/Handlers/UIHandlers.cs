
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services;
using EasyTodoListApp.Desktop.Services.Responses;
using EasyTodoListApp.Desktop.Windows;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;

namespace EasyTodoListApp.Desktop.Handlers;

public class UIHandlers(IDataService dataService) : IUIHandlers
{
    private readonly IDataService _dataService = dataService;

    public async Task TryHandleCreateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete)
    {
        string message = GetValidationMessageOrEmptyString();

        if (!message.Equals(string.Empty))
        {
            CreateTodoMessages.ShowCreateTodoValidationErrorMessage(message);
            return;
        }

        DateOnly? dueDateForDto = dueDate is null ? null : DateOnly.FromDateTime(dueDate.Value);
        CreateTodoDTO dto = new(description, dueDateForDto, isImportant, isComplete);  // TODO: We could just databind the xaml input controls to properties on the dto rather than instantiating the dto here

        try
        {
            DataServiceResponse<string> createResponse = await _dataService.TryCreateTodoAsync(dto);
            switch (createResponse.ResponseType)
            {
                case DataServiceResponseType.Success:
                    message = createResponse.Messgage;
                    CreateTodoMessages.ShowCreateTodoSucceededMessage(message);
                    System.Windows.WindowCollection a = App.Current.Windows;    // TODO: I dont like doing UI stuff like this from the page model
                    foreach (object? w in a)
                    {
                        if (w is Window window)
                        {
                            if (window.GetType().Equals(typeof(CreateTodoWindow)))
                            {
                                window.Close();
                                break;
                            }
                        }
                    }
                    return;
                case DataServiceResponseType.Failure:
                    message = createResponse.Messgage;
                    CreateTodoMessages.ShowCreateTodoFailedMessage(message);  // TODO: These messages arent very elegant and involve too much UI work in the page model
                    return;
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

            if (string.IsNullOrEmpty(description))
            {
                message = "Description is required!";
            }
            else if (Regex.Match(description, @"^\s +$").Success)
            {
                message = "Description cannot be only whitespace characters!";
            }
            else if (description.Length > 100)
            {
                message = "Description must be 100 or fewer characters!";
            }

            return message;
        }
    }

    public async Task TryHandleDeleteTodoAsync(Guid id)
    {
        try
        {
            DataServiceResponse<TodoDTO> findTodoResponse = await _dataService.TryGetTodoByIdOrThrowHttpExAsync(id);
            if (findTodoResponse.Data is null || findTodoResponse.Data is not TodoDTO todo)
            {
                DeleteTodoMessages.ShowTodoToDeleteNotFoundMessage();
                return;
            }
            else
            {
                if (todo.IsImportant)
                {
                    DeleteTodoMessages.ShowCannotDeleteImportantTodoMessage();
                    return;
                }
                else
                {
                    MessageBoxResult confirmed = DeleteTodoMessages.ShowConfirmDeleteTodoMessage(todo.Description);
                    if (confirmed.Equals(MessageBoxResult.No)) { return; }
                    else
                    {
                        DataServiceResponse<string> deleteResponse = await _dataService.TryDeleteTodoAsync(id);
                        switch (deleteResponse.ResponseType)
                        {
                            case DataServiceResponseType.Failure:
                                DeleteTodoMessages.ShowDeleteTodoFailedMessage(deleteResponse.Messgage);
                                return;
                            case DataServiceResponseType.Success:
                                DeleteTodoMessages.ShowDeleteTodoSucceededMessage(deleteResponse.Messgage);
                                // TODO: Refresh
                                return;
                        }
                    }
                }
            }

        }
        catch (HttpRequestException ex)
        {
            string message = $"Delete todo failed, the server response was status {ex.StatusCode}";
            DeleteTodoMessages.ShowDeleteTodoErrorMessage(message);
        }
    }
}
