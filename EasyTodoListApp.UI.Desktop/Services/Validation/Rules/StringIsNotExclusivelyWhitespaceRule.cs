
using EasyTodoListApp.UI.Desktop.Services.Validation.Interfaces;
using System.Text.RegularExpressions;

namespace EasyTodoListApp.UI.Desktop.Services.Validation.Rules;

public class StringIsNotExclusivelyWhitespaceRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = string.Empty;
    public bool Check(T value) => value is string s && !Regex.Match(s, @"^\s +$").Success;
}
