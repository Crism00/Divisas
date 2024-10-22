using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Divisas.ViewModels
{
    public class ConfiguracionViewModel
    {
        [Required]
        [MaxLength(255)]
        public string Logotipo { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreNegocio { get; set; }

        [Required]
        [MaxLength(100)]
        public string Colonia { get; set; }

        [Required]
        [MaxLength(100)]
        public string Calle { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; }

        [Required]
        [MaxLength(10)]
        public string CodigoPostal { get; set; }

        [Required]
        [MaxLength(100)]
        public string Estado { get; set; }

        [Required]
        [MaxLength(15)]
        public string Telefono { get; set; }

        // Constructor para inicializar las propiedades
        public ConfiguracionViewModel()
        {
            Logotipo = string.Empty;
            NombreNegocio = string.Empty;
            Colonia = string.Empty;
            Calle = string.Empty;
            Ciudad = string.Empty;
            CodigoPostal = string.Empty;
            Estado = string.Empty;
            Telefono = string.Empty;
        }
    }
}