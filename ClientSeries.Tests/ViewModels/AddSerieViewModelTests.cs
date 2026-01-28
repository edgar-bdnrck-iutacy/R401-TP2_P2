using System.Collections.Generic;
using System.Threading.Tasks;
using ClientSeries.Models;
using ClientSeries.Services;
using ClientSeries.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientSeries.Tests.ViewModels;

[TestClass]
public class AddSerieViewModelTests
{
    private class FakeWs : IService
    {
        public bool PostResult { get; set; } = true;

        public Task<bool> DeleteSerieAsync(int idSerie) => Task.FromResult(true);
        public Task<List<Serie>?> GetAllSeriesAsync() => Task.FromResult<List<Serie>?>(new List<Serie>());
        public Task<Serie?> GetSerieAsync(int idSerie) => Task.FromResult<Serie?>(null);
        public Task<bool> PostSerieAsync(Serie serie) => Task.FromResult(PostResult);
        public Task<bool> PutSerieAsync(int idSerie, Serie serie) => Task.FromResult(true);
    }

    private class FakeDialog : IDialogService
    {
        public string? LastTitle { get; private set; }
        public string? LastMessage { get; private set; }

        public Task ShowMessageAsync(string title, string message)
        {
            LastTitle = title;
            LastMessage = message;
            return Task.CompletedTask;
        }
    }

    [TestMethod]
    public void Ctor_InitializesSerieToAdd()
    {
        var vm = new AddSerieViewModel(new FakeWs(), new FakeDialog());
        Assert.IsNotNull(vm.SerieToAdd);
    }

    [TestMethod]
    public void AddCommand_CanExecute_False_WhenTitleEmpty()
    {
        var vm = new AddSerieViewModel(new FakeWs(), new FakeDialog());
        vm.SerieToAdd.Titre = "   ";
        Assert.IsFalse(vm.AddCommand.CanExecute(null));
    }

    [TestMethod]
    public async Task AddCommand_Execute_ShowsSuccessAndResetsForm_WhenPostOk()
    {
        var ws = new FakeWs { PostResult = true };
        var dlg = new FakeDialog();
        var vm = new AddSerieViewModel(ws, dlg);

        vm.SerieToAdd.Titre = "Test";
        Assert.IsTrue(vm.AddCommand.CanExecute(null));

        vm.AddCommand.Execute(null);
        await Task.Delay(50); // laisse le temps à l'async command de finir

        Assert.AreEqual("Succès", dlg.LastTitle);
        Assert.IsTrue(dlg.LastMessage?.Contains("ajoutée") ?? false);

        // reset: on a bien une nouvelle instance
        Assert.AreEqual(null, vm.SerieToAdd.Titre);
    }

    [TestMethod]
    public async Task AddCommand_Execute_ShowsError_WhenPostFails()
    {
        var ws = new FakeWs { PostResult = false };
        var dlg = new FakeDialog();
        var vm = new AddSerieViewModel(ws, dlg);

        vm.SerieToAdd.Titre = "Test";

        vm.AddCommand.Execute(null);
        await Task.Delay(50);

        Assert.AreEqual("Erreur", dlg.LastTitle);
    }
}
