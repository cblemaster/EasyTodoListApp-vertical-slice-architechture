
using System.Text.RegularExpressions;

namespace EasyTodoListApp.Domain;

public record Descriptor(string Value, bool IsRequired, bool IsAllowAllWhitespace, int? MaxLength)
{
    public static Descriptor CreateOrThrowArgException(string Value, bool IsRequired, bool IsAllowAllWhitespace, int? MaxLength)
    {
        Descriptor newDescriptor = new Descriptor(Value, IsRequired, IsAllowAllWhitespace, MaxLength);
        (bool IsValid, string ErrorMessage) = newDescriptor.Validate();
        return IsValid ? newDescriptor : throw new ArgumentException(ErrorMessage, nameof(Value));
    }

    private (bool IsValid, string ErrorMessage) Validate()
    {
        bool isValid = true;
        string errorMessage = string.Empty;
        if (IsRequired && string.IsNullOrEmpty(Value))
        {
            isValid = false;
            errorMessage = "Value is required.";
        }
        else if (!IsAllowAllWhitespace && Regex.Match(Value, @"^\s +$").Success)
        {
            isValid = false;
            errorMessage = "Value cannot consist of only whitespace characters.";
        }
        else if (MaxLength.HasValue && Value.Length > MaxLength.Value)
        {
            isValid = false;
            errorMessage = $"Value must be {MaxLength} or fewer characters.";
        }
        return (isValid, errorMessage);
    }
}
