using Microsoft.UI.Xaml.Controls;
using R401_TP2_P2.Services;
using System;
using System.Threading.Tasks;

namespace ClientSeries.Services;

public class DialogService : IDialogService
{
    public async Task ShowMessageAsync(string title, string message)
    {
        var dlg = new ContentDialog
        {
            Title = title,
            Content = message,
            CloseButtonText = "OK",
            XamlRoot = App.MainWindow!.Content.XamlRoot
        };

        await dlg.ShowAsync();
    }
}
