
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
        (bool IsValid, string error) = ValidateDescription(description);

        if (!IsValid)
        {
            CreateTodoMessages.ShowCreateTodoValidationErrorMessage(error);
            return;
        }

        DateOnly? dueDateForDto = dueDate is null ? null : DateOnly.FromDateTime(dueDate.Value);
        CreateTodoDTO dto = new(description, dueDateForDto, isImportant, isComplete);  // TODO: We could just databind the xaml input controls to properties on the dto rather than instantiating the dto here
        string message;

        try
        {
            DataServiceResponse<string> createResponse = await _dataService.TryCreateTodoAsync(dto);
            switch (createResponse.ResponseType)
            {
                case DataServiceResponseType.Success:
                    message = createResponse.Messgage;
                    CreateTodoMessages.ShowCreateTodoSucceededMessage(message);
                    System.Windows.WindowCollection a = App.Current.Windows;
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
                    CreateTodoMessages.ShowCreateTodoFailedMessage(message);
                    return;
            }
        }
        catch (HttpRequestException ex)
        {
            message = $"Create todo failed, the server response was status {ex.StatusCode}";
            CreateTodoMessages.ShowCreateTodoErrorMessage(message);
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
            else if (todo.IsImportant)
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
        catch (HttpRequestException ex)
        {
            string message = $"Delete todo failed, the server response was status {ex.StatusCode}";
            DeleteTodoMessages.ShowDeleteTodoErrorMessage(message);
        }
    }

    public async Task TryHandleUpdateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete, Guid id)
    {
        DataServiceResponse<TodoDTO> findTodoResponse = await _dataService.TryGetTodoByIdOrThrowHttpExAsync(id);
        if (findTodoResponse.Data is null || findTodoResponse.Data is not TodoDTO todo)
        {
            UpdateTodoMessages.ShowTodoToUpdateNotFoundMessage();
            return;
        }
        else
        {
            if (todo.IsComplete)
            {
                UpdateTodoMessages.ShowCannotUpdateCompletedTodoMessage();
                return;
            }

            DateOnly? dueDateForDto = dueDate is null ? null : DateOnly.FromDateTime(dueDate.Value);
            UpdateTodoDTO dto = new(description, dueDateForDto, isImportant, isComplete, id);  // TODO: We could just databind the xaml input controls to properties on the dto rather than instantiating the dto here
            (bool IsValid, string error) = ValidateDescription(dto.Description);

            if (!IsValid)
            {
                UpdateTodoMessages.ShowUpdateTodoValidationErrorMessage(error);
                return;
            }

            try
            {
                string message;
                DataServiceResponse<string> updateResponse = await _dataService.TryUpdateTodoAsync(dto, id);
                switch (updateResponse.ResponseType)
                {
                    case DataServiceResponseType.Success:
                        message = updateResponse.Messgage;
                        UpdateTodoMessages.ShowUpdateTodoSucceededMessage(message);
                        System.Windows.WindowCollection a = App.Current.Windows;
                        foreach (object? w in a)
                        {
                            if (w is Window window)
                            {
                                if (window.GetType().Equals(typeof(UpdateTodoWindow)))
                                {
                                    window.Close();
                                    break;
                                }
                            }
                        }
                        return;
                    case DataServiceResponseType.Failure:
                        message = updateResponse.Messgage;
                        UpdateTodoMessages.ShowUpdateTodoFailedMessage(message);
                        return;
                }
            }
            catch (HttpRequestException ex)
            {
                string message = $"Update todo failed, the server response was status {ex.StatusCode}";
                UpdateTodoMessages.ShowUpdateTodoErrorMessage(message);
            }
        }
    }

    public async Task TryMarkTodoIncompleteAsync(Guid id)
    {
        DataServiceResponse<TodoDTO> findTodoResponse = await _dataService.TryGetTodoByIdOrThrowHttpExAsync(id);
        if (findTodoResponse.Data is null || findTodoResponse.Data is not TodoDTO todo)
        {
            UpdateTodoMessages.ShowTodoToUpdateNotFoundMessage();
            return;
        }
        else if (!todo.IsComplete)
        {
            UpdateTodoMessages.ShowMarkTodoIncompleteTodoIsNotCompleteMessage();
            return;
        }
        else
        {
            MarkTodoIncompleteDTO dto = new(id);
            string message;

            try
            {
                DataServiceResponse<string> markIncompleteResponse = await _dataService.TryMarkTodoIncompleteAsync(dto, id);
                switch (markIncompleteResponse.ResponseType)
                {
                    case DataServiceResponseType.Success:
                        message = markIncompleteResponse.Messgage;
                        MarkTodoIncompleteMessages.ShowMarkTodoIncompleteSucceededMessage(message);
                        return;
                    case DataServiceResponseType.Failure:
                        message = markIncompleteResponse.Messgage;
                        MarkTodoIncompleteMessages.ShowMarkTodoIncompleteFailedMessage(message);
                        return;
                }
            }
            catch (HttpRequestException ex)
            {
                message = $"Mark todo incomplete failed, the server response was status {ex.StatusCode}";
                MarkTodoIncompleteMessages.ShowMarkTodoIncompleteErrorMessage(message);
            }
        }
    }

    private static (bool IsValid, string error) ValidateDescription(string description)
    {
        bool isValid = true;
        string message = string.Empty;

        if (string.IsNullOrEmpty(description))
        {
            isValid = false;
            message = "Description is required!";
        }
        else if (Regex.Match(description, @"^\s +$").Success)
        {
            isValid = false;
            message = "Description cannot be only whitespace characters!";
        }
        else if (description.Length > 100)
        {
            isValid = false;
            message = "Description must be 100 or fewer characters!";
        }

        return (isValid, message);
    }
}
