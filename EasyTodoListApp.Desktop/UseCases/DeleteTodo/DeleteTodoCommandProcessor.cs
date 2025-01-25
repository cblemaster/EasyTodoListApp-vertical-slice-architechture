
using CommunityToolkit.Mvvm.Messaging;
using EasyTodoListApp.Desktop.Interfaces;
using EasyTodoListApp.Desktop.Messages;
using EasyTodoListApp.Desktop.Models;
using EasyTodoListApp.Desktop.Services.Responses;
using System.Net.Http;
using System.Windows;

namespace EasyTodoListApp.Desktop.UseCases.DeleteTodo;

public class DeleteTodoCommandProcessor(IDataService dataService) : IDeleteTodoCommandProcessor
{
    private readonly IDataService _dataService = dataService;
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
                            WeakReferenceMessenger.Default.Send(new TodosChangedMessage("data deleted..."));
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
}
