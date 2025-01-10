
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
        public partial ValidatableObject<string> Description { get => field; set; } = new ValidatableObject<string>() { Value = string.Empty };
        
        [ObservableProperty]
        public partial DateOnly? DueDate { get => field; set => field = value; }
                
        [ObservableProperty]
        public partial bool IsImportant { get =>  field; set; }
        
        [ObservableProperty]
        public partial bool IsComplete { get => field; set; }
                
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        public partial bool IsValid { get =>  field; set; }
        
        public override string About => "page for creating todos";
        public static DateOnly? DefaultDate => DateOnly.FromDateTime(DateTime.Today);

        public CreateTodoPageModel(IDataService dataService) : base(dataService)
        {
            Description.Validations.Add(new StringIsNotNullOrEmptyRule<string> { ValidationMessage = "Description is required." });
            Description.Validations.Add(new StringIsNotExclusivelyWhitespaceRule<string> { ValidationMessage = "Description cannot be exclusively whitespace." });
            Description.Validations.Add(new StringDoesNotExceedLengthOfOneHundredRule<string> { ValidationMessage = "Description must be 100 characters or fewer." });
        }
        
        public override void LoadDataAsync() { }

        [RelayCommand]
        public static async Task CancelAsync() => await Shell.Current.Navigation.PopModalAsync();

        [RelayCommand(CanExecute = nameof(IsValid))]
        public async Task SaveAsync() { }

        [RelayCommand]
        public void ValidateDescription() => IsValid = Description.Validate();
    }
}
