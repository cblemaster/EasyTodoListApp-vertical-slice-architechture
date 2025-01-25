using System.Text.RegularExpressions;

namespace EasyTodoListApp.Desktop.Models;

public static class DescriptionValidator
{
    public static (bool IsValid, string error) ValidateDescription(string description)
    {
        bool isValid = true;
        string message = string.Empty;

        if (string.IsNullOrEmpty(description))
        {
            isValid = false;
            message = "Description is required!";
        }
        else if (Regex.Match(description, @"^\s +$").Success)
        {
            isValid = false;
            message = "Description cannot be only whitespace characters!";
        }
        else if (description.Length > 100)
        {
            isValid = false;
            message = "Description must be 100 or fewer characters!";
        }

        return (isValid, message);
    }
}
