using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSQR.Models
{
    public class PpmReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ahora identity
        public int Id { get; set; }

        [Required]
        public int ReporteId { get; set; }   // <-- FK a qcReport(Id)

        [ForeignKey(nameof(ReporteId))]
        public Reporte Reporte { get; set; } // navegación 1:1

        public DateTime Fecha { get; set; }
        public string? Linea { get; set; }

        [Required, StringLength(100)]
        public string? Customer { get; set; }

        public int CustomerClaimNumber { get; set; }
        [StringLength(100)]
        public string? MotherFactory { get; set; }
        public string? NumParteAfectado { get; set; }
        [StringLength(100)]
        public string? CustomerPartNumber { get; set; }
        public int? CuantosP { get; set; }
        public string? Lote { get; set; }
        public TipoReporte Tipo { get; set; }
        public string? Mileage { get; set; }
        public string? TituloProblema { get; set; }
        public string? InvestigationReport { get; set; }
        public DateTime? DateOfClose { get; set; }
        public bool ImpactoPPM { get; set; }
        public string? Comentarios { get; set; }
        public ResponsabilidadOpciones Responsabilidad { get; set; }
    }
}
