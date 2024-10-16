using Divisas.Models;
using Divisas.ViewsModels;
namespace Divisas;

public partial class App : Application
{
	static MonedaDatabase? database;
	
	public App()
	{
		InitializeComponent();
		if (Application.Current != null)
		{
			Application.Current.UserAppTheme = AppTheme.Light;
		}

        _ = SeedDatabaseAsync();
		MainPage = new AppShell();
	}

	public static MonedaDatabase Database
    {
        get
        {
            if (database == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MonedaSQLite.db3");
                database = new MonedaDatabase(dbPath);
            }
            return database;
        }
    }

	private async Task SeedDatabaseAsync()
	{
		var monedasExistentes = await App.Database.GetMonedasAsync();
		if (monedasExistentes.Count == 0)
		{
			// Agrega algunas monedas de ejemplo
			await App.Database.SaveMonedaAsync(new Monedas { Nombre = "USD", ActivoDivisa = true});
			await App.Database.SaveMonedaAsync(new Monedas { Nombre = "MXN", ActivoDivisa = true});
			await App.Database.SaveMonedaAsync(new Monedas { Nombre = "EUR", ActivoDivisa = true});
			await App.Database.SaveMonedaAsync(new Monedas { Nombre = "JPY", ActivoDivisa = true});
		}
	}
}
