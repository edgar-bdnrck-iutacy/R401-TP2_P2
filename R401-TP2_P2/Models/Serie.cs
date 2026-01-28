namespace ClientSeries.Models;

public class Serie
{
    public int IdSerie { get; set; }          // adapte si l’API renvoie "idSerie" ou "id"
    public string? Titre { get; set; }
    public string? Resume { get; set; }
    public int NbSaisons { get; set; }        // IMPORTANT: int (pas int?) pour binding NumberBox
    public int NbEpisodes { get; set; }
    public int AnneeCreation { get; set; }
    public string? Chaine { get; set; }

    // IMPORTANT: constructeur vide pour la désérialisation
    public Serie() { }
}
