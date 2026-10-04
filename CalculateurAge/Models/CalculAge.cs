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
        int age = aujourdhui.Year - naissance.Year;
        // Si l'anniversaire n'est pas encore passé cette année, on retire une année.
        if (naissance.Date > aujourdhui.Date.AddYears(-age)) age--;
        return age;
    }

    public static bool EstMajeur(int age) => age >= AgeMajorite;
}
