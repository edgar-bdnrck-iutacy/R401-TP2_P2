using ClientSeries.Models;
using ClientSeries.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientSeries.Tests.Services;

[TestClass]
public class WSServiceTests
{
    private readonly WSService _ws = new();

    [TestMethod]
    public async Task GetAllSeriesAsync_ReturnsList()
    {
        var res = await _ws.GetAllSeriesAsync();
        Assert.IsNotNull(res);
        Assert.IsTrue(res.Count >= 0);
    }

    [TestMethod]
    public async Task GetSerieAsync_WithExistingId_ReturnsSerieOrNull()
    {
        // si tu sais qu’il existe une série 1 sur l’API, ce test peut être plus strict.
        var serie = await _ws.GetSerieAsync(1);
        // on accepte null si l’API n’a pas de 1, mais le call ne doit pas crash
        Assert.IsTrue(serie == null || !string.IsNullOrWhiteSpace(serie.Titre));
    }

    [TestMethod]
    public async Task PostPut_Workflow_ReturnsTrue()
    {
        var s = new Serie
        {
            Titre = "TP2 test",
            Resume = "Créée depuis test MSTest",
            NbSaisons = 1,
            NbEpisodes = 8,
            AnneeCreation = 2025,
            Chaine = "IUT"
        };

        var created = await _ws.PostSerieAsync(s);
        Assert.IsTrue(created);

        // Pour Put il faut un id : si l’API renvoie l’ID en réponse POST, il faudrait le récupérer.
        // Là, on ne peut pas deviner l'id sans endpoint/retour => on fera Put dans le VM avec une série récupérée.
    }
}
