using Divisas.Database;
using Divisas.Models;
using Microsoft.EntityFrameworkCore;

namespace Divisas;

public partial class App : Application
{
    static DivisasDbContext? context;

    public App()
    {
        InitializeComponent();
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        // Inicializa el contexto de la base de datos
        context = new DivisasDbContext();
        context.Database.EnsureCreated(); // Asegura que la base de datos existe

        _ = SeedDatabaseAsync();
        MainPage = new AppShell();
    }

    private async Task SeedDatabaseAsync()
	{
		if (context != null)
		{
			var monedasExistentes = await context.Monedas.ToListAsync();
			if (monedasExistentes.Count == 0)
			{
				await context.Monedas.AddAsync(new Monedas { Nombre = "USD", ActivoDivisa = true });
				await context.Monedas.AddAsync(new Monedas { Nombre = "MXN", ActivoDivisa = true });
				await context.Monedas.AddAsync(new Monedas { Nombre = "EUR", ActivoDivisa = true });
				await context.Monedas.AddAsync(new Monedas { Nombre = "JPY", ActivoDivisa = true });
				await context.SaveChangesAsync(); 
			}
		}
		else
		{
			throw new InvalidOperationException("El contexto de la base de datos no se ha inicializado.");
		}
	}


}
