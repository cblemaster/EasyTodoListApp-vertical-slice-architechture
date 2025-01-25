
using CommunityToolkit.Mvvm.Messaging;
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;
using System.Net.Http;
using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.UpdateTodo;

public class UpdateTodoCommandProcessor(IDataService dataService) : IUpdateTodoCommandProcessor
{
    private readonly IDataService _dataService = dataService;
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
            (bool IsValid, string error) = DescriptionValidator.ValidateDescription(dto.Description);

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
                        WeakReferenceMessenger.Default.Send(new TodosChangedMessage("data updated..."));
                        WindowCollection a = App.Current.Windows;
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
}
