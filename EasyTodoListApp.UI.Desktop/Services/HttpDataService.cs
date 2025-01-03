
using EasyTodoListApp.UI.Desktop.Models;
using System.Net;
using System.Text.Json;

namespace EasyTodoListApp.UI.Desktop.Services;

public class HttpDataService : IHttpDataService
{
    private readonly HttpClient _client;
    private const string BASE_URI = "https://localhost:7194/todos";

    internal HttpDataService() => _client = new HttpClient { BaseAddress = new Uri(BASE_URI) };

    public void CreateTodo(CreateTodoCommand command) { throw new NotImplementedException(); }
    public void DeleteTodo(Guid id) { throw new NotImplementedException(); }
    public async Task<IEnumerable<Todo>> GetAllTodosCompleteAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/complete");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<Todo>> GetAllTodosDueTodayAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/duetoday");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<Todo>> GetAllTodosImportantAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/important");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<Todo>> GetAllTodosNotCompleteAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/notcomplete");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<IEnumerable<Todo>> GetAllTodosOverdueAsync()
    {
        HttpResponseMessage response = await _client.GetAsync("/overdue");
        return await DeserializeTodoListAsync(response.Content);
    }
    public async Task<Todo> GetTodoByIdAsync(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"/{id}");
        if (response.StatusCode.Equals(HttpStatusCode.NotFound))
        {
            throw new NotImplementedException();   // TODO: fix this!
        }
        return await DeserializeTodoAsync(response.Content);
    }
    public void ToggleTodoCompletion(Guid id) { throw new NotImplementedException(); }
    public void ToggleTodoImportance(Guid id) { throw new NotImplementedException(); }
    public void UpdateTodo(UpdateTodoCommand command, Guid id) { throw new NotImplementedException(); }

    private static async Task<Todo> DeserializeTodoAsync(HttpContent content)
    {
        JsonDocument todo = JsonDocument.Parse(await content.ReadAsStringAsync());
        JsonElement todoRoot = todo.RootElement;
        return CreateTodoFromJsonElement(todoRoot);
    }
    private static async Task<IEnumerable<Todo>> DeserializeTodoListAsync(HttpContent content)
    {
        byte[] bytes = await content.ReadAsByteArrayAsync();
        Utf8JsonReader jsonReader = new(bytes);
        JsonDocument json = JsonDocument.ParseValue(ref jsonReader);
        JsonElement.ArrayEnumerator elements = json.RootElement.EnumerateArray();

        List<Todo> todos = [];

        foreach (JsonElement element in elements)
        {
            Todo todo = CreateTodoFromJsonElement(element);
            todos.Add(todo);
        }
        return todos.AsEnumerable();
    }
    private static Todo CreateTodoFromJsonElement(JsonElement element)
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

        return new Todo(description, dueDate, isImportant, isComplete, createDate, updateDate, id);
    }
}
