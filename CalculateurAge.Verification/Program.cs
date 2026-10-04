// Vérification console des règles métier et des ViewModels.
// Lancer : dotnet run --project CalculateurAge.Verification
using CalculateurAge.Models;
using CalculateurAge.Services;
using CalculateurAge.ViewModels;

int echecs = 0;
void Verifier(bool condition, string description)
{
    Console.WriteLine($"{(condition ? "OK  " : "ECHEC")} {description}");
    if (!condition) echecs++;
}

var aujourdhui = new DateTime(2026, 10, 4);

// --- Règles métier ---
Verifier(CalculAge.Age(new DateTime(2000, 10, 4), aujourdhui) == 26, "Âge le jour de l'anniversaire");
Verifier(CalculAge.Age(new DateTime(2000, 10, 5), aujourdhui) == 25, "Âge la veille de l'anniversaire");
Verifier(CalculAge.EstMajeur(18) && !CalculAge.EstMajeur(17), "Seuil de majorité à 18 ans");
Verifier(CalculAge.JoursAvantAnniversaire(new DateTime(2000, 10, 4), aujourdhui) == 0, "Anniversaire aujourd'hui = 0 jour");
Verifier(CalculAge.JoursAvantAnniversaire(new DateTime(2000, 10, 5), aujourdhui) == 1, "Anniversaire demain = 1 jour");
Verifier(CalculAge.JoursAvantAnniversaire(new DateTime(2000, 10, 3), aujourdhui) == 364, "Anniversaire passé hier = 364 jours");
Verifier(CalculAge.Age(new DateTime(2004, 2, 29), new DateTime(2027, 2, 28)) == 23, "Né un 29/02 : un an de plus le 28/02 d'une année non bissextile");

// --- ViewModel principal ---
var navigation = new NavigationFactice();
var vm = new CalculateurViewModel(navigation, () => aujourdhui);

Verifier(!vm.CalculerCommand.CanExecute(null), "Calculer grisé si nom vide");
Verifier(!vm.EffacerCommand.CanExecute(null), "Effacer grisé si formulaire vierge");
Verifier(!vm.VoirDetailCommand.CanExecute(null), "Voir le détail grisé avant calcul");

var notifiees = new List<string?>();
vm.PropertyChanged += (_, e) => notifiees.Add(e.PropertyName);
vm.Nom = "Thomas";
Verifier(notifiees.Contains(nameof(vm.Nom)), "PropertyChanged levé pour Nom");
Verifier(vm.CalculerCommand.CanExecute(null), "Calculer actif dès qu'un nom est saisi");

vm.DateNaissance = aujourdhui.AddDays(1);
Verifier(!vm.CalculerCommand.CanExecute(null), "Calculer grisé si date de naissance future");

vm.DateNaissance = new DateTime(2010, 10, 10);
vm.CalculerCommand.Execute(null);
Verifier(vm.Resultat == "Thomas, vous avez 15 ans", $"Résultat : « {vm.Resultat} »");
Verifier(vm.Statut == "Mineur", "Statut Mineur");
Verifier(vm.ProchainAnniversaire == "Prochain anniversaire dans 6 jours", $"Anniversaire : « {vm.ProchainAnniversaire} »");
Verifier(vm.ResultatVisible, "Résultat visible après calcul");
Verifier(vm.Historique.Count == 1 && vm.HistoriqueVisible, "Historique : 1 entrée");

vm.VoirDetailCommand.Execute(null);
Verifier(navigation.Dernier?.Nom == "Thomas", "Navigation vers le détail avec le bon ResultatAge");

vm.EffacerCommand.Execute(null);
Verifier(vm.Nom == "" && !vm.ResultatVisible && vm.Resultat == "", "Effacer remet le formulaire à zéro");
Verifier(vm.Historique.Count == 1, "Effacer conserve l'historique");
Verifier(!vm.VoirDetailCommand.CanExecute(null), "Voir le détail grisé après Effacer");

vm.ViderHistoriqueCommand.Execute(null);
Verifier(vm.Historique.Count == 0 && !vm.HistoriqueVisible, "Vider l'historique");

// --- ViewModel de détail ---
var detail = new ResultatViewModel(navigation);
detail.Resultat = ResultatAge.Calculer("Awa", new DateTime(1990, 1, 1), aujourdhui);
Verifier(detail.Message == "Awa, vous avez 36 ans" && detail.Statut == "Majeur", "ResultatViewModel expose message et statut");
detail.RetourCommand.Execute(null);
Verifier(navigation.Retours == 1, "RetourCommand délègue au service de navigation");

Console.WriteLine(echecs == 0 ? "\nTout est vert." : $"\n{echecs} échec(s).");
return echecs == 0 ? 0 : 1;

// Faux service de navigation : enregistre les appels au lieu de changer de page.
class NavigationFactice : INavigationService
{
    public ResultatAge? Dernier { get; private set; }
    public int Retours { get; private set; }
    public Task AllerVersResultatAsync(ResultatAge resultat) { Dernier = resultat; return Task.CompletedTask; }
    public Task RetourAsync() { Retours++; return Task.CompletedTask; }
}
