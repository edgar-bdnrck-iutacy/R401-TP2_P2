using ClientSeries.Views;
using Microsoft.UI.Xaml;

namespace ClientSeries;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        RootFrame.Navigate(typeof(AddSeriePage));
    }
}
