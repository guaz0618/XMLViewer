using System.Globalization;
using System.Windows.Data;
using System.Xml.Linq;

namespace XMLViewer;

/// <summary>
/// Converts an <see cref="XElement"/> to a collection of <see cref="XObject"/> nodes.
/// </summary>
[ValueConversion(typeof(XElement), typeof(IEnumerable<XObject>))]
public class XElementTreeNodesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is XElement element)
        {
            return GetNodes(element);
        }

        throw new ArgumentException("Value must be an XElement", nameof(value));
    }

    private IEnumerable<XObject> GetNodes(XElement element)
    {
        foreach (var attr in element.Attributes())
        {
            yield return attr;
        }

        var text = element.Nodes().OfType<XText>().FirstOrDefault();
        if (text != null)
        {
            yield return text;
        }

        foreach (var child in element.Elements())
        {
            yield return child;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}