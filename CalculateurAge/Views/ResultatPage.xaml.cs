using CalculateurAge.Models;
using CalculateurAge.Services;
using CalculateurAge.ViewModels;

namespace CalculateurAge.Views;

// IQueryAttributable : Shell appelle ApplyQueryAttributes avec les
// paramètres de navigation. Contrairement à [QueryProperty], aucun
// passage par une chaîne d'URL : l'objet ResultatAge arrive intact.
public partial class ResultatPage : ContentPage, IQueryAttributable
{
    private readonly ResultatViewModel _viewModel;

    public ResultatPage(ResultatViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    // Plomberie uniquement : on transmet la donnée reçue au ViewModel.
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(ShellNavigationService.CleResultat, out var valeur))
            _viewModel.Resultat = valeur as ResultatAge;
    }
}
