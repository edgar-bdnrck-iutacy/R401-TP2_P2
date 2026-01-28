using System.Threading.Tasks;

namespace R401_TP2_P2.Services;

public interface IDialogService
{
    Task ShowMessageAsync(string title, string message);
}
