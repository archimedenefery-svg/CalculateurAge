using CalculateurAge.Models;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    // Contient l'ÉTAT de l'écran et les ACTIONS possibles.

    // Source de la date du jour : injectable pour pouvoir tester le ViewModel.
    private readonly Func<DateTime> _aujourdhui;

    // Champs privés : la vraie donnée.
    private string _nom = "";
    private DateTime _dateNaissance;
    private string _resultat = "";
    private bool _resultatVisible;
    private string _statut = "";

    // Propriétés publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value)) CalculerCommand.Rafraichir(); }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set => SetField(ref _dateNaissance, value);
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set => SetField(ref _resultatVisible, value);
    }

    // « Majeur » ou « Mineur ».
    public string Statut
    {
        get => _statut;
        set => SetField(ref _statut, value);
    }

    // Lié à Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }

    public CalculateurViewModel() : this(() => DateTime.Today) { }

    internal CalculateurViewModel(Func<DateTime> aujourdhui)
    {
        _aujourdhui = aujourdhui;
        _dateNaissance = _aujourdhui().AddYears(-20);
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom));
    }

    // La logique métier : aucun contrôle d'interface ici.
    private void Calculer()
    {
        int age = CalculAge.Age(DateNaissance, _aujourdhui());
        Resultat = $"{Nom}, vous avez {age} ans";
        Statut = CalculAge.EstMajeur(age) ? "Majeur" : "Mineur";
        ResultatVisible = true;
    }
}
