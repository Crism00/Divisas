using System.ComponentModel.DataAnnotations;

namespace Divisas.Models
{
    public class Config
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
         public string Logotipo { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreNegocio { get; set; }

        [Required]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ciudad { get; set; }

        [Required]
        [MaxLength(100)]
        public string Estado { get; set; }
        
        public Config getConfiguraton(){
            return this;
        }
        // Constructor para inicializar las propiedades
    }
}