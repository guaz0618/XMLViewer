using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace XMLViewer;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public XDocument Document { get; set; }

    public ClickNodeAction OnClickNode { get; set; } = ClickNodeAction.CopyNodeValue;

    public ICommand OpenFileCommand { get; }

    public ICommand ClickNodeCommand { get; }

    public MainWindowViewModel()
    {
        OpenFileCommand = new RelayCommand(OpenFile);
        ClickNodeCommand = new RelayCommand<object>(ClickNode);
    }

    private async void OpenFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            Document = XDocument.Load(openFileDialog.FileName);
        }
    }   
    
    private async void ClickNode(object node)
    {
        switch (node)
        {
            case XElement element:
                ClickXElement(element);
                break;
            case XAttribute attribute:
                ClickXAttribute(attribute);
                break;
            case XText text:
                ClickXText(text);
                break;
        }
    }

    #region ClickXElement Methods
    private void ClickXElement(XElement element)
    {
        switch (OnClickNode)
        {
            case ClickNodeAction.CopyNodeValue:
                Clipboard.SetText(element.Value);
                MessageBox.Show("Value copied to clipboard.");
                break;
            case ClickNodeAction.CopyNodeXPath:
                Clipboard.SetText(GetXElementXPath(element));
                MessageBox.Show("XPath copied to clipboard.");
                break;
        }
    }

    private string GetXElementXPath(XElement element)
    {
        string path = element.Name.LocalName;
        XElement parent = element.Parent;

        while (parent != null)
        {
            path = parent.Name.LocalName + "/" + path;
            parent = parent.Parent;
        }

        return path;
    }
    #endregion ClickXElement Methods

    #region ClickXAttribute Methods
    private void ClickXAttribute(XAttribute attribute)
    {
        switch (OnClickNode)
        {
            case ClickNodeAction.CopyNodeValue:
                Clipboard.SetText(attribute.Value);
                MessageBox.Show("Value copied to clipboard.");
                break;
            case ClickNodeAction.CopyNodeXPath:
                Clipboard.SetText(GetXAttributeXPath(attribute));
                MessageBox.Show("XPath copied to clipboard.");
                break;
        }
    }

    private string GetXAttributeXPath(XAttribute attribute)
    {
        string path = "@" + attribute.Name.LocalName;
        XElement parent = attribute.Parent;

        while (parent != null)
        {
            path = parent.Name.LocalName + "/" + path;
            parent = parent.Parent;
        }

        return path;
    }
    #endregion ClickXAttribute Methods

    #region ClickXText Methods
    private void ClickXText(XText text)
    {
        switch (OnClickNode)
        {
            case ClickNodeAction.CopyNodeValue:
                Clipboard.SetText(text.Value);
                MessageBox.Show("Value copied to clipboard.");
                break;
            case ClickNodeAction.CopyNodeXPath:
                Clipboard.SetText(GetXTextXPath(text));
                MessageBox.Show("XPath copied to clipboard.");
                break;
        }
    }

    private string GetXTextXPath(XText text)
    {
        string path = "text()";
        XElement parent = text.Parent;

        while (parent != null)
        {
            path = parent.Name.LocalName + "/" + path;
            parent = parent.Parent;
        }

        return path;
    }
    #endregion ClickXText Methods
}