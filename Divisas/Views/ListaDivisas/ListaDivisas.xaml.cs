using Divisas.Controllers;
using Divisas.Models;

namespace Divisas.Views;

public partial class ListaDivisas : ContentPage
{
    public ListaDivisas()
    {
        LoadMonedas();
        InitializeComponent();
    }

    private async void LoadMonedas()
    {
        var monedasController = new MonedasController();
        var monedas = await monedasController.GetMonedasAsync();
        MonedasCollectionView.ItemsSource = monedas;

    }
}