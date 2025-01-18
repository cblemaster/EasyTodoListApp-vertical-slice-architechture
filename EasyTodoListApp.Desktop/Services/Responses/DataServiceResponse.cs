
namespace EasyTodoListApp.Desktop.Services.Responses;

public class DataServiceResponse<T>
{
    public string Messgage { get; set; } = string.Empty;
    public DataServiceResponseType ResponseType { get; set; }
    public T? Data { get; set; }
}
