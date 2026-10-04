# Calculateur d'âge — .NET MAUI (Routing et MVVM)

TP1 de l'atelier de développement mobile, enrichi pour l'Activité 6.
Application .NET MAUI (.NET 10) qui calcule l'âge à partir d'un nom et d'une date de naissance, construite en trois phases puis enrichie en respectant le MVVM.

## Historique des commits

| Commit | Contenu |
|---|---|
| Étape 0 | Projet .NET MAUI vierge, sans sample content |
| Phase A | Version code-behind (`x:Name`, `Clicked`) |
| Phase B | Seconde page `ResultatPage`, route Shell, paramètres `[QueryProperty]` |
| Phase C | Réécriture MVVM : `BaseViewModel`, `RelayCommand`, `CalculateurViewModel`, bindings |
| Fonctionnalités 1 à 5 | Une fonctionnalité par commit (voir ci-dessous) |
| Vérification | Projet console qui compile et teste les ViewModels sans MAUI |

## Fonctionnalités ajoutées (Activité 6)

1. **Statut Majeur / Mineur** — propriété `Statut` du ViewModel, seuil dans `CalculAge.AgeMajorite`.
2. **Jours avant le prochain anniversaire** — `ProchainAnniversaire` (« Joyeux anniversaire ! », « demain », « dans N jours »), 29 février géré. Le bouton Calculer est grisé si la date est dans le futur, et le `DatePicker` est borné par `DateMaximale`.
3. **Commande Effacer** — `EffacerCommand` remet le formulaire à zéro ; grisée tant qu'il n'y a rien à effacer.
4. **Historique des calculs** — `ObservableCollection<ResultatAge>` (20 entrées max), affichée par `BindableLayout`, avec `ViderHistoriqueCommand`.
5. **Résultat transmis à `ResultatPage` par le ViewModel** — `VoirDetailCommand` appelle `INavigationService` ; l'objet `ResultatAge` est passé tel quel (dictionnaire Shell + `IQueryAttributable`) au lieu de chaînes dans l'URL. `ResultatPage` a son propre `ResultatViewModel`. Pages, ViewModels et service sont fournis par l'injection de dépendances (`MauiProgram`).

## Respect du MVVM

- Aucun ViewModel ne contient `Label`, `Entry`, `Button`, `DisplayAlert` ni `Shell`.
- Les code-behind se limitent à `InitializeComponent()`, à l'affectation du `BindingContext` et, pour `ResultatPage`, à la transmission du paramètre de navigation au ViewModel.
- **Preuve** : `CalculateurAge.Verification` est une application console *sans MAUI* qui compile les dossiers `ViewModels/`, `Models/` et `INavigationService`, puis vérifie leur comportement avec un faux service de navigation.

```bash
dotnet run --project CalculateurAge.Verification
```

## Structure

```
CalculateurAge/
  Models/         CalculAge.cs, ResultatAge.cs           (règles métier pures)
  ViewModels/     BaseViewModel.cs, RelayCommand.cs,
                  CalculateurViewModel.cs, ResultatViewModel.cs
  Services/       INavigationService.cs, ShellNavigationService.cs
  Views/          ResultatPage.xaml(.cs)
  MainPage.xaml(.cs), AppShell.xaml(.cs), MauiProgram.cs
CalculateurAge.Verification/   Program.cs (vérifications console)
```

## Lancer l'application

Ouvrir `CalculateurAge.sln` dans Visual Studio (charge de travail .NET MAUI installée), choisir un émulateur Android, puis Démarrer.
