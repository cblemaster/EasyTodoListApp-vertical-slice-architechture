
using EasyTodoListApp.UI.Desktop.Models;
using System.Text.Json;

namespace EasyTodoListApp.UI.Desktop.Services;

public class HttpDataService : IDataService
{
    private readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7194";

    public HttpDataService() => _client = new HttpClient { BaseAddress = new Uri(BASE_URI) };

    public async void CreateTodoAsync(CreateTodoDTO dto)
    {
        StringContent content = new(JsonSerializer.Serialize(dto));
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
    public async void UpdateTodoAsync(UpdateTodoDTO dto, Guid id)
    {
        var mappedDto = new
        {
            Description = new
            {
                Value = dto.Description,
                IsRequired = true,
                IsAllowAllWhitespace = false,
                MaxLength = 100
            },
            DueDate = dto.DueDate,
            Identifier = new
            {
                Value = id
            }
        };

        StringContent content = new(JsonSerializer.Serialize(mappedDto));
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
        JsonProperty descriptionRoot = element.EnumerateObject().Single(t => t.NameEquals("description"));
        JsonProperty dueDateRoot = element.EnumerateObject().Single(t => t.NameEquals("dueDate"));
        JsonProperty isImportantRoot = element.EnumerateObject().Single(t => t.NameEquals("isImportant"));
        JsonProperty isCompleteRoot = element.EnumerateObject().Single(t => t.NameEquals("isComplete"));
        JsonProperty datesRoot = element.EnumerateObject().Single(t => t.NameEquals("dates"));
        JsonProperty identifierRoot = element.EnumerateObject().Single(t => t.NameEquals("identifier"));

        string description = descriptionRoot.Value.EnumerateObject().Single(t => t.NameEquals("value")).Value.GetString() ?? string.Empty;
        DateOnly? dueDate = dueDateRoot.Value.ValueKind.Equals(JsonValueKind.Null) ? null : DateOnly.FromDateTime(dueDateRoot.Value.GetDateTime());
        bool isImportant = isImportantRoot.Value.GetBoolean();
        bool isComplete = isCompleteRoot.Value.GetBoolean();
        DateTime createDate = datesRoot.Value.EnumerateObject().Single(t => t.NameEquals("createDate")).Value.GetDateTime();

        JsonElement datesElement = datesRoot.Value.EnumerateObject().Single(t => t.NameEquals("updateDate")).Value;
        DateTime? updateDate = datesElement.ValueKind.Equals(JsonValueKind.Null) ? null : datesElement.GetDateTime();

        Guid id = identifierRoot.Value.EnumerateObject().Single(t => t.NameEquals("value")).Value.GetGuid();

        return new TodoDTO(description, dueDate, isImportant, isComplete, createDate, updateDate, id);
    }
}
