using System.Collections.ObjectModel;
using Divisas.Controllers;
using Divisas.Models;

namespace Divisas.Views;

public partial class ListaDivisas : ContentPage
{
    private Monedas _currentMoneda; // Para almacenar la moneda actualmente en edición

    public ListaDivisas()
    {
        InitializeComponent(); // Inicializa la interfaz
        LoadMonedas();         // Carga los datos
    }

    private async void LoadMonedas()
    {
        var monedasController = new MonedasController();
        var monedas = await monedasController.GetMonedasAsync();
        MonedasCollectionView.ItemsSource = monedas.ToList();
    }

    public async void SaveMoneda(object sender, EventArgs e)
    {
        var moneda = new Monedas
        {
            Nombre = NombreEntry.Text,
            V_Compra = float.Parse(CompraEntry.Text),
            V_Venta = float.Parse(VentaEntry.Text),
        };

        var monedasController = new MonedasController();

        // Verifica si se está editando una moneda
        if (_currentMoneda != null)
        {
            moneda.Id = _currentMoneda.Id; // Asegúrate de que el Id esté presente
            await monedasController.EditMonedaAsync(moneda);
            _currentMoneda = null; // Reinicia la moneda actual después de guardar
            SaveButton.Text = "Guardar tipo de cambio"; // Restaura el texto del botón
        }
        else
        {
            await monedasController.SaveNewMonedaAsync(moneda);
        }
        ResetCampos();
        LoadMonedas();
    }

    public void EditMoneda(object sender, EventArgs e)
    {
        // Obtén la moneda desde el CommandParameter
        var moneda = (Monedas)((Button)sender).CommandParameter;

        // Rellena los campos con los datos de la moneda seleccionada
        NombreEntry.Text = moneda.Nombre;
        CompraEntry.Text = moneda.V_Compra.ToString();
        VentaEntry.Text = moneda.V_Venta.ToString();


        // Guarda la moneda actual para su edición
        _currentMoneda = moneda;
        SaveButton.Text = "Guardar edición"; // Cambia el texto del botón
    }
    private void ResetCampos(){
        NombreEntry.Text = string.Empty;
        CompraEntry.Text = string.Empty;
        VentaEntry.Text = string.Empty;
    }
}
