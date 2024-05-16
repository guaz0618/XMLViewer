using System.Windows;
using System.Windows.Input;

namespace XMLViewer;

/// <summary>
/// Provides a command that can be attached to a UI element to execute when clicked.
/// </summary>
public static class ClickCommand
{
    public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
        "Command",
        typeof(ICommand),
        typeof(ClickCommand),
        new PropertyMetadata(null, OnCommandChanged));

    public static void SetCommand(DependencyObject d, ICommand value) => d.SetValue(CommandProperty, value);

    public static ICommand GetCommand(DependencyObject d) => (ICommand)d.GetValue(CommandProperty);

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached(
        "CommandParameter",
        typeof(object),
        typeof(ClickCommand));

    public static void SetCommandParameter(DependencyObject d, object value) => d.SetValue(CommandParameterProperty, value);

    public static object GetCommandParameter(DependencyObject d) => d.GetValue(CommandParameterProperty);

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            element.MouseLeftButtonDown += (sender, args) =>
            {
                var command = GetCommand((DependencyObject)sender);
                var parameter = GetCommandParameter((DependencyObject)sender);
                if (command?.CanExecute(parameter) == true)
                {
                    command.Execute(parameter);
                }
            };
        }
    }
}