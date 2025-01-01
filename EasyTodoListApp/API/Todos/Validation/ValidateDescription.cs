
using EasyTodoListApp.Domain;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EasyTodoListApp.API.Todos.Validation;

public static class ValidateDescription
{
    public static (bool IsValid, string ErrorMessage) Validate(string description)
    {
        bool isValid = true;
        string errorMessage = string.Empty;

        if (Todo.IS_DESCRIPTION_REQUIRED && string.IsNullOrEmpty(description))
        {
            isValid = false;
            errorMessage = "Description is required!";
        }
        else if (Todo.IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED && Regex.Match(description, @"^\s +$").Success)
        {
            isValid = false;
            errorMessage = "Description cannot be only whitespace characters!";
        }
        else if (Todo.MAX_LENGTH_FOR_DESCRIPTION > description.Length)
        {
            isValid = false;
            errorMessage = $"Description must be { Todo.MAX_LENGTH_FOR_DESCRIPTION } or fewer characters!";
        }

        return (isValid, errorMessage);
    }
}
