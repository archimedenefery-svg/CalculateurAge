namespace CalculateurAge.Models;

// Règles métier du calcul d'âge : fonctions pures, sans état ni interface.
// Testables isolément (voir le projet CalculateurAge.Verification).
public static class CalculAge
{
    // Seuil de majorité utilisé par l'application.
    public const int AgeMajorite = 18;

    // Âge en années révolues à la date "aujourdhui".
    public static int Age(DateTime naissance, DateTime aujourdhui)
    {
        DateTime jour = aujourdhui.Date;
        int age = jour.Year - naissance.Year;
        // Si l'anniversaire n'est pas encore passé cette année, on retire une année.
        if (AnniversaireEn(naissance, jour.Year) > jour) age--;
        return age;
    }

    public static bool EstMajeur(int age) => age >= AgeMajorite;

    // Nombre de jours avant le prochain anniversaire (0 = c'est aujourd'hui).
    public static int JoursAvantAnniversaire(DateTime naissance, DateTime aujourdhui)
    {
        DateTime jour = aujourdhui.Date;
        DateTime prochain = AnniversaireEn(naissance, jour.Year);
        if (prochain < jour) prochain = AnniversaireEn(naissance, jour.Year + 1);
        return (prochain - jour).Days;
    }

    public static string MessageAnniversaire(int jours) => jours switch
    {
        0 => "Joyeux anniversaire !",
        1 => "Prochain anniversaire demain",
        _ => $"Prochain anniversaire dans {jours} jours"
    };

    // Date d'anniversaire pour une année donnée.
    // Né un 29 février : fêté le 28 février les années non bissextiles.
    private static DateTime AnniversaireEn(DateTime naissance, int annee)
        => new(annee, naissance.Month,
               Math.Min(naissance.Day, DateTime.DaysInMonth(annee, naissance.Month)));
}
