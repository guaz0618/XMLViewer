using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace XMLViewer;

/// <summary>
/// Selects a <see cref="DataTemplate"/> based on the type of the <see cref="XNode"/>.
/// </summary>
public class XNodeTemplateSelector : DataTemplateSelector
{
    public HierarchicalDataTemplate XElementTemplate { get; set; }
    public DataTemplate XAttributeTemplate { get; set; }
    public DataTemplate XTextTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is XElement)
            return XElementTemplate;
        else if (item is XAttribute)
            return XAttributeTemplate;
        else if (item is XText)
            return XTextTemplate;
        else
            return base.SelectTemplate(item, container);
    }
}