
using EasyTodoListApp.API.Todos.UseCases.CreateTodo;
using EasyTodoListApp.Domain;
using System.Text.RegularExpressions;

namespace EasyTodoListApp.API.Todos.Validation;

public static class ValidateCreateTodoCommand  // TODO: This duplicates ValidateUpdateTodoCommand; descriptor also has the same validation
{
   public static (bool IsValid, string ErrorMessage) Validate(CreateTodoCommand command)
   {
      bool isValid = true;
      string errorMessage = string.Empty;
      
      if (Todo.IS_DSCRIPTION_REQUIRED && string.IsNullOrEmpty(command.Description))
      {
         isValid = false;
         errorMessage = "Description is required.";
      }
      else if (!Todo.IS_DESCRIPTION_ALL_WHITESPACE_ALLOWED && Regex.Match(command.Description, @"^\s +$").Success)
      {
         isValid = false;
         errorMessage = "Description cannot consist of only whitespace characters.";
      }
      else if (command.Description.Length > Todo.MAX_LENGTH_FOR_DESCRIPTION)
      {
         isValid = false;
         errorMessage = $"Value must be { Todo.MAX_LENGTH_FOR_DESCRIPTION } or fewer characters.";
      }
      return (isValid, errorMessage);
   }
}
