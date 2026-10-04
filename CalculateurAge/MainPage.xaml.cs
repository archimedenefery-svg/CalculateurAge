using CalculateurAge.ViewModels;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    // Le ViewModel est fourni par l'injection de dépendances (MauiProgram) :
    // la page ne le construit plus elle-même.
    public MainPage(CalculateurViewModel viewModel)
    {
        InitializeComponent();
        // Objet dans lequel tous les {Binding} de la page
        // vont chercher leurs valeurs.
        BindingContext = viewModel;
    }
}
