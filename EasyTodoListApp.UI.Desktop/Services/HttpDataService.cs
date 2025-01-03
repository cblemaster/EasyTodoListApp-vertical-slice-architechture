
using EasyTodoListApp.UI.Desktop.Models;
using System.Text.Json;

namespace EasyTodoListApp.UI.Desktop.Services;

public class HttpDataService : IHttpDataService
{
    private readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7194";

    public HttpDataService() => _client = new HttpClient { BaseAddress = new Uri(BASE_URI) };

    public async void CreateTodoAsync(CreateTodoDTO command)
    {
        StringContent content = new(JsonSerializer.Serialize(command));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PostAsync("/todos", content);
            response.EnsureSuccessStatusCode();
            // TODO: Message to UI that the create succeeded
        }
        catch (HttpRequestException ex)
        {
            string message = $"Create todo failed, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the create failed
        }
    }
    public async void DeleteTodoAsync(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"todos/{id}");
            response.EnsureSuccessStatusCode();
            // TODO: Message to UI that the delete succeeded
        }
        catch (HttpRequestException ex)
        {
            string message = $"Delete todo failed, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the delete failed
        }
    }
    public async Task<IEnumerable<TodoDTO>> GetAllTodosCompleteAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("todos/complete");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<TodoDTO>> GetAllTodosDueTodayAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("todos/duetoday");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<TodoDTO>> GetAllTodosImportantAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("todos/important");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<TodoDTO>> GetAllTodosNotCompleteAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("todos/notcomplete");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<TodoDTO>> GetAllTodosOverdueAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("todos/overdue");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<TodoDTO> GetTodoByIdOrThrowHttpExAsync(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"todos/{id}");
            response.EnsureSuccessStatusCode();
            return await DeserializeTodoAsync(response.Content);
        }
        catch (HttpRequestException ex)
        {
            string message = $"Todo not found, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the get by id failed
            throw;
        }
    }
    public async void ToggleTodoCompletionAsync(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/todos/{id}/completion", null);
            response.EnsureSuccessStatusCode();
            // TODO: Message to UI that the toggle todo completion succeeded
        }
        catch (HttpRequestException ex)
        {
            string message = $"Toggle todo completion failed, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the toggle todo completion failed
        }
    }
    public async void ToggleTodoImportanceAsync(Guid id)
    {
        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/todos/{id}/importance", null);
            response.EnsureSuccessStatusCode();
            // TODO: Message to UI that the toggle todo importance succeeded
        }
        catch (HttpRequestException ex)
        {
            string message = $"Toggle todo importance failed, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the toggle todo importance failed
        }
    }
    public async void UpdateTodoAsync(UpdateTodoDTO command, Guid id)
    {
        StringContent content = new(JsonSerializer.Serialize(command));
        content.Headers.ContentType = new("application/json");

        try
        {
            HttpResponseMessage response = await _client.PutAsync($"/todos/{id}", content);
            response.EnsureSuccessStatusCode();
            // TODO: Message to UI that the update succeeded
        }
        catch (HttpRequestException ex)
        {
            string message = $"Update todo failed, the server response was status {ex.StatusCode}";
            // TODO: Message to UI that the update failed
        }
    }

    private static async Task<TodoDTO> DeserializeTodoAsync(HttpContent content)
    {
        JsonDocument todo = JsonDocument.Parse(await content.ReadAsStringAsync());
        JsonElement todoRoot = todo.RootElement;
        return CreateTodoFromJsonElement(todoRoot);
    }
    private static async Task<IEnumerable<TodoDTO>> DeserializeTodoListAsync(HttpContent content)
    {
        byte[] bytes = await content.ReadAsByteArrayAsync();
        Utf8JsonReader jsonReader = new(bytes);
        JsonDocument json = JsonDocument.ParseValue(ref jsonReader);
        JsonElement.ArrayEnumerator elements = json.RootElement.EnumerateArray();

        List<TodoDTO> todos = [];
        foreach (JsonElement element in elements)
        {
            TodoDTO todo = CreateTodoFromJsonElement(element);
            todos.Add(todo);
        }
        return todos.AsEnumerable();
    }
    private static TodoDTO CreateTodoFromJsonElement(JsonElement element)
    {
        string description = element.EnumerateObject().Single(t => t.Name.Equals("description", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.Object)).Value.EnumerateObject().Single(t => t.Name.Equals("value") && t.Value.ValueKind.Equals(JsonValueKind.String)).ToString();
        DateOnly? dueDate = null;
        bool? isJsonDueDateNull = element.EnumerateObject().Single(t => t.Name.Equals("dueDate", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).Value.GetString()?.Equals("null", StringComparison.CurrentCultureIgnoreCase);
        if (isJsonDueDateNull.HasValue && !isJsonDueDateNull.Value)
        {
            dueDate = DateOnly.FromDateTime(DateTime.Parse(element.EnumerateObject().Single(t => t.Name.Equals("dueDate", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).Value.ToString()));
        }

        bool isImportant = bool.Parse(element.EnumerateObject().Single(t => t.Name.Equals("isImportant", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).Value.ToString());
        bool isComplete = bool.Parse(element.EnumerateObject().Single(t => t.Name.Equals("isComplete", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).Value.ToString());
        JsonProperty dates = element.EnumerateObject().Single(t => t.Name.Equals("dates", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.Object));
        DateTime createDate = DateTime.Parse(dates.Value.EnumerateObject().Single(t => t.Name.Equals("createDate", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).ToString());
        DateTime? updateDate = null;
        bool? isJsonUpdateDateNull = dates.Value.EnumerateObject().Single(t => t.Name.Equals("updateDate", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).ToString().Equals("null", StringComparison.CurrentCultureIgnoreCase);
        if (isJsonUpdateDateNull.HasValue && !isJsonUpdateDateNull.Value)
        {
            updateDate = DateTime.Parse(dates.Value.EnumerateObject().Single(t => t.Name.Equals("updateDate", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).Value.ToString());
        }

        Guid id = Guid.Parse(element.EnumerateObject().Single(t => t.Name.Equals("identifier", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.Object)).Value.EnumerateObject().Single(t => t.Name.Equals("value", StringComparison.CurrentCultureIgnoreCase) && t.Value.ValueKind.Equals(JsonValueKind.String)).ToString());

        return new TodoDTO(description, dueDate, isImportant, isComplete, createDate, updateDate, id);
    }
}
