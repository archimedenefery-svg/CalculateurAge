using CalculateurAge.Models;
using CalculateurAge.Services;

namespace CalculateurAge.ViewModels;

// ViewModel de la page de détail : il reçoit un ResultatAge
// et expose des textes prêts à afficher.
public class ResultatViewModel : BaseViewModel
{
    private readonly INavigationService _navigation;
    private ResultatAge? _resultat;

    public ResultatViewModel(INavigationService navigation)
    {
        _navigation = navigation;
        RetourCommand = new RelayCommand(async () => await _navigation.RetourAsync());
    }

    public ResultatAge? Resultat
    {
        get => _resultat;
        set
        {
            if (!SetField(ref _resultat, value)) return;
            // Les propriétés calculées dépendent de Resultat : on les signale aussi.
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(DateNaissanceTexte));
            OnPropertyChanged(nameof(Statut));
            OnPropertyChanged(nameof(Anniversaire));
        }
    }

    public string Message => Resultat?.Message ?? "";
    public string DateNaissanceTexte => Resultat?.DateNaissanceTexte ?? "";
    public string Statut => Resultat?.Statut ?? "";
    public string Anniversaire => Resultat?.MessageAnniversaire ?? "";

    public RelayCommand RetourCommand { get; }
}
