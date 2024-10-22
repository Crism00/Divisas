namespace Divisas.Views;

public partial class ListaDivisas : ContentPage
{
	public List<string> Monedas {get; set;}
    public ListaDivisas()
    {
        InitializeComponent();
        GetTiposDeCambio();
    }

    private async void GetTiposDeCambio()
    {
        // Obtén las monedas desde la base de datos
        var monedas = await App.Database.GetMonedasAsync();
        var monedaNombres = monedas.Select(m => m.Nombre).ToList();  // Solo usar los nombres de las monedas
		Monedas = monedaNombres;
		BindingContext = this;
    }
}