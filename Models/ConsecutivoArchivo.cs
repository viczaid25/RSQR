using System;
using System.ComponentModel.DataAnnotations;

namespace RSQR.Models
{
    public class ConsecutivoArchivo
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string UnidadNegocio { get; set; }

        [Required, StringLength(10)]
        public string CodigoNegocio { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        public int Consecutivo { get; set; } = 1; // Valor por defecto

        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}