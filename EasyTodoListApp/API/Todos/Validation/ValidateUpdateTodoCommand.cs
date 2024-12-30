
using EasyTodoListApp.API.Todos.UseCases.UpdateTodo;
using EasyTodoListApp.Domain;
using System.Text.RegularExpressions;

namespace EasyTodoListApp.API.Todos.Validation;

public static class ValidateUpdateTodoCommand
{
   public static (bool IsValid, string ErrorMessage) Validate(UpdateTodoCommand command)
   {
      bool isValid = true;
      string errorMessage = string.Empty;

      if (Todo.IS_DSCRIPTION_REQUIRED && string.IsNullOrEmpty(command.Description))
      {
         isValid = false;
         errorMessage = "Value is required.";
      }
      else if (!Todo.IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED && Regex.Match(command.Description, @"^\s +$").Success)
      {
         isValid = false;
         errorMessage = "Value cannot consist of only whitespace characters.";
      }
      else if (command.Description.Length > Todo.MAX_LENGTH_FOR_DESCRIPTION)
      {
         isValid = false;
         errorMessage = $"Value must be { Todo.MAX_LENGTH_FOR_DESCRIPTION } or fewer characters.";
      }
      return (isValid, errorMessage);
   }
}
