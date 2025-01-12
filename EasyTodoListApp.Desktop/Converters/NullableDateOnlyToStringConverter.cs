
using System.Globalization;
using System.Windows.Data;

namespace EasyTodoListApp.Desktop.Converters;

public sealed class NullableDateOnlyToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is null ? "none" : ((DateOnly)value).ToString("D");

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
