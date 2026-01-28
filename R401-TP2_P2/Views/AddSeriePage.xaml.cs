using ClientSeries.Services;
using ClientSeries.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace ClientSeries.Views;

public sealed partial class AddSeriePage : Page
{
    public AddSeriePage()
    {
        InitializeComponent();

        // simple: instanciation directe (comme TP1)
        DataContext = new AddSerieViewModel(new WSService(), new DialogService());
    }
}
