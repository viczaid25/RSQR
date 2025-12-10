using System;

namespace RSQR.Models
{
    public class QcPpmReport
    {
        public int Id { get; set; }                 // PK (IDENTITY esperado)
        public int? ReporteId { get; set; }         // Vincula con PpmReport.Id
        public string Linea { get; set; } = default!;
        public DateTime Fecha { get; set; }         // Fecha de referencia del caso
        public int CuantosP { get; set; }           // Defectuosas
        public bool IsActive { get; set; } = true;  // Soft delete
        public DateTime UpdatedAt { get; set; }     // Auditoría
    }
}
