using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CanvasReminders.Views;

public partial class CourseView : UserControl
{
    public CourseView()
    {
        InitializeComponent();
    }
    
    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox listBox)
        {
            listBox.SelectedItem = null;
        }
    }
}