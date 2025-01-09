﻿
using EasyTodoListApp.UI.Desktop.Services.Validation.Interfaces;

namespace EasyTodoListApp.UI.Desktop.Services.Validation.Rules;

public class StringDoesNotExceedLengthOfOneHundredRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = string.Empty;
    public bool Check(T value) => value is string s && !(s.Length > 100);
}
