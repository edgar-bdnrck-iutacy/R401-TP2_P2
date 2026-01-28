using ClientSeries.Models;
using ClientSeries.Services;
using ClientSeries.ViewModels;
using System;
using System.Threading.Tasks;

namespace ClientSeries.ViewModels;

public class AddSerieViewModel : ObservableObject
{
    private readonly IService _ws;
    private readonly IDialogService _dialog;

    private Serie _serieToAdd = new Serie();

    public Serie SerieToAdd
    {
        get => _serieToAdd;
        set => SetProperty(ref _serieToAdd, value);
    }

    public AsyncRelayCommand AddCommand { get; }

    public AddSerieViewModel(IService ws, IDialogService dialog)
    {
        _ws = ws;
        _dialog = dialog;

        // 1) Command d'abord (sinon elle est null quand SerieToAdd déclenche le setter)
        AddCommand = new AsyncRelayCommand(AddAsync);

        // 2) Ensuite seulement l'objet bindé
        SerieToAdd = new Serie();

        // (optionnel) force l’état du bouton à jour au démarrage
        AddCommand.RaiseCanExecuteChanged();
    }

    private async Task AddAsync()
    {
        if (string.IsNullOrWhiteSpace(SerieToAdd.Titre))
        {
            await _dialog.ShowMessageAsync(
                "Validation",
                "Le titre est obligatoire pour ajouter une série."
            );
            return;
        }

        try
        {
            // (Optionnel) normalisation soft
            SerieToAdd.Titre = SerieToAdd.Titre?.Trim();
            SerieToAdd.Resume = SerieToAdd.Resume?.Trim();
            SerieToAdd.Chaine = SerieToAdd.Chaine?.Trim();

            var ok = await _ws.PostSerieAsync(SerieToAdd);

            if (ok)
            {
                await _dialog.ShowMessageAsync("Succès", "Série ajoutée avec succès.");

                // reset formulaire
                SerieToAdd = new Serie();
                AddCommand.RaiseCanExecuteChanged();
            }
            else
            {
                await _dialog.ShowMessageAsync("Erreur", "L’ajout a échoué (réponse API non OK).");
            }
        }
        catch (Exception ex)
        {
            await _dialog.ShowMessageAsync("Erreur", $"Une erreur est survenue : {ex.Message}");
        }
    }
}
