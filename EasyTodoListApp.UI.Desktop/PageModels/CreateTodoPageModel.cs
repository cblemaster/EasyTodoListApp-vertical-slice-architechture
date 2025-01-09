
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasyTodoListApp.UI.Desktop.Services;
using EasyTodoListApp.UI.Desktop.Services.Validation;
using EasyTodoListApp.UI.Desktop.Services.Validation.Rules;

namespace EasyTodoListApp.UI.Desktop.PageModels
{
    public partial class CreateTodoPageModel : PageModelBase
    {
        [ObservableProperty]
        public ValidatableObject<string> _description = new() { Value = string.Empty };
        [ObservableProperty]
        public DateOnly? _dueDate;
        [ObservableProperty]
        public bool _isImportant;
        [ObservableProperty]
        public bool _isComplete;
        [ObservableProperty]
        
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private bool _isValid;
        
        

        public CreateTodoPageModel(IDataService dataService) : base(dataService)
        {
            Description.Validations.Add(new StringIsNotNullOrEmptyRule<string> { ValidationMessage = "Description is required." });
            Description.Validations.Add(new StringIsNotExclusivelyWhitespaceRule<string> { ValidationMessage = "Description cannot be exclusively whitespace." });
            Description.Validations.Add(new StringDoesNotExceedLengthOfOneHundredRule<string> { ValidationMessage = "Description must be 100 characters or fewer." });
        }

        public override string About => "page for creating todos";
        public override void LoadDataAsync() { }

        [RelayCommand]
        public async Task CancelAsync() => await Shell.Current.Navigation.PopModalAsync();

        [RelayCommand(CanExecute = nameof(IsValid))]
        public async Task SaveAsync() { }

        [RelayCommand]
        public void ValidateDescription() => IsValid = Description.Validate();
    }
}
