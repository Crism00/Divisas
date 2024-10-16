using System.Threading.Tasks;
using Microsoft.Maui.Controls; // Asegúrate de que esta línea esté presente para usar DisplayAlert

namespace Divisas.Helpers
{
    public static class DialogsHelper
    {
        private static Page? _currentPage;

        // Método para establecer la página actual
        public static void SetCurrentPage(Page page)
        {
            _currentPage = page;
        }

        public static async Task ShowLoadingMessage(string message)
        {
            await DisplayAlert("Cargando", message, "OK");
        }

        public static async Task ShowErrorMessage(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

        public static async Task ShowWarningMessage(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

        public static async Task ShowSuccessMessage(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

        public static void HideLoadingMessage()
        {
            // En MAUI, no necesitas implementar la lógica para ocultar un diálogo de carga
            // ya que esto se maneja con el ciclo de vida de la UI.
        }

        // Método auxiliar para llamar a DisplayAlert
        private static Task DisplayAlert(string title, string message, string cancel)
        {
            return _currentPage?.DisplayAlert(title, message, cancel) ?? Task.CompletedTask;
        }
    }
}
