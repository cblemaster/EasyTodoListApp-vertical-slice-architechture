using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.UI.Desktop.Services;
using EasyTodoListApp.UI.Desktop.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTodoListApp.UI.Desktop.PageModels
{
    public partial class CreateTodoPageModel(IDataService dataService) : PageModelBase(dataService)
    {
        [ObservableProperty]
        public ValidatableObject<string> _description = new() { Value = string.Empty };
        [ObservableProperty]
        public DateOnly? _dueDate;
        [ObservableProperty]
        public bool _isImportant;
        [ObservableProperty]
        public bool _isComplete;
        
        public override string About => "page for creating todos";
        public override void LoadDataAsync() { }

        [RelayCommand]
        public async Task CancelAsync() { }

        [RelayCommand]
        public async Task SaveAsync() { }
    }
}
