using CalculateurAge.Services;
using CalculateurAge.ViewModels;
using CalculateurAge.Views;
using Microsoft.Extensions.Logging;

namespace CalculateurAge;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Injection de dépendances : chaque page reçoit son ViewModel,
		// chaque ViewModel reçoit le service de navigation.
		builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
		builder.Services.AddSingleton<CalculateurViewModel>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<ResultatViewModel>();
		builder.Services.AddTransient<ResultatPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
