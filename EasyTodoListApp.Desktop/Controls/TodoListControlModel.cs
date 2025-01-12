
using EasyTodoListApp.Desktop.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

namespace EasyTodoListApp.Desktop.Controls;

public class TodoListControlModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged = delegate { };

    private ReadOnlyObservableCollection<TodoDTO> _todos = new([]);

    public ReadOnlyObservableCollection<TodoDTO> Todos
    {
        get => _todos;
        set
        {
            if (!value.Equals(_todos))
            {
                _todos = value;
                PropertyChanged!(this, new PropertyChangedEventArgs(nameof(Todos)));
            }
        }
    }

    private async void LoadDataAsync()
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("https://localhost:7194/")
        };

        try
        {
            HttpResponseMessage response = await client.GetAsync("/notcomplete");
            if (response.IsSuccessStatusCode && response.Content is not null)
            {
                //Todos = new ReadOnlyObservableCollection<Todo>(response.Content.ReadFromJsonAsAsyncEnumerable<Todo>().ToBlockingEnumerable().ToList());
            }
        }
        catch (Exception)
        {
            throw; // TODO }
        }
    }
}
