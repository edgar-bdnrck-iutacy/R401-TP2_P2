using System.Threading.Tasks;

namespace ClientSeries.Services;

public interface IDialogService
{
    Task ShowMessageAsync(string title, string message);
}
