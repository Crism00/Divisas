using System.ComponentModel.DataAnnotations;

namespace Divisas.Models
{
    public class Configuracion
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; }

        public bool ActivoDivisa { get; set; }

        [MaxLength(255)]
        public string? Logotipo { get; set; } // Ruta o URL del logotipo

        [MaxLength(100)]
        public string? Direccion { get; set; }

        [MaxLength(50)]
        public string? Ciudad { get; set; }

        [MaxLength(50)]
        public string? Estado { get; set; }
    }
}