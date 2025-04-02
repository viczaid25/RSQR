using System;
using System.ComponentModel.DataAnnotations;

namespace RSQR.Models

{
    public class PpmReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Linea { get; set; }

        [Required]
        [StringLength(100)]
        public string Customer { get; set; }
        public int CustomerClaimNumber { get; set; }

        [StringLength(100)]
        public string MotherFactory { get; set; }
        public string? NumParteAfectado { get; set; }

        [StringLength(100)]
        public string CustomerPartNumber { get; set; }
        public string? CuantosP { get; set; }
        public string? Lote { get; set; }
        public TipoReporte Tipo { get; set; }

        [Range(0, int.MaxValue)]
        public string Mileage { get; set; }
        public string? TituloProblema { get; set; }

        public string InvestigationReport { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfClose { get; set; }

        [Range(0, double.MaxValue)]
        public bool ImpactoPPM { get; set; }
        public string? Comentarios { get; set; }

        public ResponsabilidadOpciones Responsabilidad { get; set; }
    }
}
