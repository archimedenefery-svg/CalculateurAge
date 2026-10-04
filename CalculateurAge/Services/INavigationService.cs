using CalculateurAge.Models;

namespace CalculateurAge.Services;

// Abstraction de la navigation : le ViewModel demande « afficher le détail »
// sans connaître Shell ni aucune page. Il reste compilable hors de MAUI.
public interface INavigationService
{
    Task AllerVersResultatAsync(ResultatAge resultat);
    Task RetourAsync();
}
