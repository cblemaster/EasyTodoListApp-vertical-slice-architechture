﻿
using System.Globalization;
using System.Windows.Data;

namespace EasyTodoListApp.Desktop.Converters;

public sealed class BoolToYesNoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is null ? false : ((bool)value ? "Yes" : "No");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
