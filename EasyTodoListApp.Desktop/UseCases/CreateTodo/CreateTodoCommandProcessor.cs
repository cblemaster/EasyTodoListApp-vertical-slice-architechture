
using CommunityToolkit.Mvvm.Messaging;
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;
using System.Net.Http;
using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.CreateTodo;

public class CreateTodoCommandProcessor(IDataService dataService) : ICreateTodoCommandProcessor
{
    private readonly IDataService _dataService = dataService;
    public async Task TryHandleCreateTodoAsync(string description, DateTime? dueDate, bool isImportant, bool isComplete)
    {
        (bool IsValid, string error) = DescriptionValidator.ValidateDescription(description);

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
                    WeakReferenceMessenger.Default.Send(new TodosChangedMessage("data created..."));
                    WindowCollection a = App.Current.Windows;
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
}
