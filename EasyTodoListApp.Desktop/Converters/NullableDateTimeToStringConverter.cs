
using System.Globalization;
using System.Windows.Data;

namespace EasyTodoListApp.Desktop.Converters;

public sealed class NullableDateTimeToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is null ? "none" : ((DateTime)value).ToString("D");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
