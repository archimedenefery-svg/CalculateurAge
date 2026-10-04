using CalculateurAge.Models;
using CalculateurAge.Views;

namespace CalculateurAge.Services;

// Implémentation MAUI de la navigation, basée sur Shell.
// Seul endroit du projet qui connaît à la fois les routes et les données.
public class ShellNavigationService : INavigationService
{
    // Clé du paramètre de navigation lu par ResultatPage.
    public const string CleResultat = "resultat";

    // L'objet ResultatAge est transmis tel quel (dictionnaire),
    // et non plus sérialisé en texte dans l'URL.
    public Task AllerVersResultatAsync(ResultatAge resultat)
        => Shell.Current.GoToAsync(nameof(ResultatPage),
            new Dictionary<string, object> { [CleResultat] = resultat });

    // ".." = revenir à la page précédente.
    public Task RetourAsync() => Shell.Current.GoToAsync("..");
}
