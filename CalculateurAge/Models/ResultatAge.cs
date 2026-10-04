namespace CalculateurAge.Models;

// Résultat d'un calcul : donnée immuable, stockée dans l'historique
// et transmise à la page de détail.
public record ResultatAge(string Nom, DateTime DateNaissance, int Age, int JoursAvantAnniversaire)
{
    public bool EstMajeur => CalculAge.EstMajeur(Age);
    public string Statut => EstMajeur ? "Majeur" : "Mineur";
    public string Message => $"{Nom}, vous avez {Age} ans";
    public string MessageAnniversaire => CalculAge.MessageAnniversaire(JoursAvantAnniversaire);
    public string DateNaissanceTexte => $"Né(e) le {DateNaissance:dd/MM/yyyy}";

    // Ligne affichée dans l'historique.
    public string Resume => $"{Nom} · {Age} ans · {Statut}";

    public static ResultatAge Calculer(string nom, DateTime naissance, DateTime aujourdhui)
        => new(nom.Trim(), naissance.Date,
               CalculAge.Age(naissance, aujourdhui),
               CalculAge.JoursAvantAnniversaire(naissance, aujourdhui));
}
