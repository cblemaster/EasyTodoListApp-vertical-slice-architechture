
using CommunityToolkit.Mvvm.Messaging;
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;
using EasyTodoListApp.Desktop.UseCases.UpdateTodo;
using System.Net.Http;

namespace EasyTodoListApp.Desktop.UseCases.MarkTodoIncomplete;

public class MarkTodoIncompleteCommandProcessor(IDataService dataService) : IMarkTodoIncompleteCommandProcessor
{
    private readonly IDataService _dataService = dataService;
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
                        WeakReferenceMessenger.Default.Send(new TodosChangedMessage("data updated..."));
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
}
