using Divisas.Controllers;
using Divisas.Models;

namespace Divisas.Views;

public partial class Configuracion : ContentPage
{	
	private readonly ConfiguracionController _configuracionController;
	public Configuracion()
	{
		InitializeComponent();
		_configuracionController = new ConfiguracionController();
		GetConfiguracion();
	}
	private void OnSaveClicked(object sender, EventArgs e)
	{
		var configuracion = new Config
		{
			NombreNegocio = txtBusinessName.Text,
			Direccion = txtAddress.Text,
			Ciudad = txtCity.Text,
			Estado = txtState.Text,
			Logotipo = "logo.png"
		};
		_configuracionController.SaveConfiguracion(configuracion);
		GetConfiguracion();
	}

	private void GetConfiguracion(){
		var configuracion = _configuracionController.GetConfiguracion();
		if (configuracion != null)
		{
			txtBusinessName.Text = configuracion.NombreNegocio;
			txtAddress.Text = configuracion.Direccion;
			txtCity.Text = configuracion.Ciudad;
			txtState.Text = configuracion.Estado;
		}
	}
}