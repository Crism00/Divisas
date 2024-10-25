using System.ComponentModel.DataAnnotations;

namespace Divisas.Models
{
    public class Monedas
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? Nombre { get; set; }

        public bool ActivoDivisa { get; set; }

        public float V_Compra { get; set; }
        public float V_Venta { get; set; }
    }
}
