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
				await context.Monedas.AddAsync(new Monedas { Nombre = "USD", ActivoDivisa = true, V_Compra = 20.0f, V_Venta = 21.0f });
				await context.Monedas.AddAsync(new Monedas { Nombre = "MXN", ActivoDivisa = true, V_Compra = 1.0f, V_Venta = 1.0f });
				await context.Monedas.AddAsync(new Monedas { Nombre = "EUR", ActivoDivisa = true, V_Compra = 22.0f, V_Venta = 23.0f });
				await context.Monedas.AddAsync(new Monedas { Nombre = "JPY", ActivoDivisa = true,  V_Compra = 0.18f, V_Venta = 0.19f });
				await context.SaveChangesAsync(); 
			}
		}
		else
		{
			throw new InvalidOperationException("El contexto de la base de datos no se ha inicializado.");
		}
	}


}
