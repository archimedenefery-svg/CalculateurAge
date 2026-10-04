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
    private string _prochainAnniversaire = "";

    // Propriétés publiques : ce que le XAML voit.
    public string Nom
    {
        get => _nom;
        set
        {
            if (SetField(ref _nom, value))
            {
                CalculerCommand.Rafraichir();
                EffacerCommand.Rafraichir();
            }
        }
    }

    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set { if (SetField(ref _dateNaissance, value)) CalculerCommand.Rafraichir(); }
    }

    public string Resultat
    {
        get => _resultat;
        set => SetField(ref _resultat, value);
    }

    public bool ResultatVisible
    {
        get => _resultatVisible;
        set { if (SetField(ref _resultatVisible, value)) EffacerCommand.Rafraichir(); }
    }

    // « Majeur » ou « Mineur ».
    public string Statut
    {
        get => _statut;
        set => SetField(ref _statut, value);
    }

    // Message sur le prochain anniversaire.
    public string ProchainAnniversaire
    {
        get => _prochainAnniversaire;
        set => SetField(ref _prochainAnniversaire, value);
    }

    // Borne supérieure du DatePicker : on ne naît pas dans le futur.
    public DateTime DateMaximale => _aujourdhui();

    // Lié à Button.Command dans le XAML.
    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel() : this(() => DateTime.Today) { }

    internal CalculateurViewModel(Func<DateTime> aujourdhui)
    {
        _aujourdhui = aujourdhui;
        _dateNaissance = DateParDefaut();
        CalculerCommand = new RelayCommand(
            Calculer,
            () => !string.IsNullOrWhiteSpace(Nom)
                  && DateNaissance.Date <= _aujourdhui().Date);
        // Rien à effacer tant que le formulaire est vierge.
        EffacerCommand = new RelayCommand(
            Effacer,
            () => !string.IsNullOrEmpty(Nom) || ResultatVisible);
    }

    // La logique métier : aucun contrôle d'interface ici.
    private void Calculer()
    {
        int age = CalculAge.Age(DateNaissance, _aujourdhui());
        Resultat = $"{Nom}, vous avez {age} ans";
        Statut = CalculAge.EstMajeur(age) ? "Majeur" : "Mineur";
        ProchainAnniversaire = CalculAge.MessageAnniversaire(
            CalculAge.JoursAvantAnniversaire(DateNaissance, _aujourdhui()));
        ResultatVisible = true;
    }

    // Remet le formulaire et le résultat à leur état initial.
    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateParDefaut();
        Resultat = "";
        Statut = "";
        ProchainAnniversaire = "";
        ResultatVisible = false;
    }

    private DateTime DateParDefaut() => _aujourdhui().AddYears(-20);
}
