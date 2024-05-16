using System.Globalization;
using System.Windows.Data;

namespace XMLViewer;

/// <summary>
/// Converts an enum value to a boolean based on a parameter value.
/// </summary>
[ValueConversion(typeof(Enum), typeof(bool))]
public class EnumToBooleanConverter : IValueConverter
{
    public Type EnumType { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum enumValue && parameter is string parameterValue)
        {
            return enumValue.ToString() == parameterValue;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue && boolValue)
        {
            return Enum.Parse(EnumType, parameter.ToString());
        }

        return Binding.DoNothing;
    }
}