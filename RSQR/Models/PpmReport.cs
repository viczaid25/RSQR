using System;
using System.ComponentModel.DataAnnotations;

namespace RSQR.Models
{
    /// <summary>
    /// Representa un reporte PPM (Parts Per Million) para el control de calidad y gestión de reclamos de clientes.
    /// </summary>
    /// <remarks>
    /// Este modelo almacena información detallada sobre problemas de calidad reportados por clientes,
    /// incluyendo investigación, responsabilidad y métricas de impacto.
    /// </remarks>
    public class PpmReport
    {
        /// <summary>
        /// Identificador único del reporte en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Fecha de creación del reporte.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Línea de producción o área relacionada con el problema.
        /// </summary>
        public string? Linea { get; set; }

        /// <summary>
        /// Nombre del cliente que reporta el problema.
        /// </summary>
        /// <remarks>
        /// Campo obligatorio con longitud máxima de 100 caracteres.
        /// </remarks>
        [Required]
        [StringLength(100)]
        public string? Customer { get; set; }

        /// <summary>
        /// Número de reclamo asignado por el cliente.
        /// </summary>
        public int CustomerClaimNumber { get; set; }

        /// <summary>
        /// Fábrica de origen del producto con problema.
        /// </summary>
        /// <remarks>
        /// Longitud máxima de 100 caracteres.
        /// </remarks>
        [StringLength(100)]
        public string? MotherFactory { get; set; }

        /// <summary>
        /// Número de parte afectado (opcional).
        /// </summary>
        public string? NumParteAfectado { get; set; }

        /// <summary>
        /// Número de parte según el cliente.
        /// </summary>
        /// <remarks>
        /// Longitud máxima de 100 caracteres.
        /// </remarks>
        [StringLength(100)]
        public string? CustomerPartNumber { get; set; }

        /// <summary>
        /// Cantidad de partes afectadas (opcional).
        /// </summary>
        public string? CuantosP { get; set; }

        /// <summary>
        /// Lote o batch del producto con problema (opcional).
        /// </summary>
        public string? Lote { get; set; }

        /// <summary>
        /// Tipo de reporte según la clasificación interna.
        /// </summary>
        public TipoReporte Tipo { get; set; }

        /// <summary>
        /// Kilometraje o horas de uso (para productos en servicio).
        /// </summary>
        /// <remarks>
        /// Debe ser un valor positivo.
        /// </remarks>
        [Range(0, int.MaxValue)]
        public string? Mileage { get; set; }

        /// <summary>
        /// Título descriptivo del problema (opcional).
        /// </summary>
        public string? TituloProblema { get; set; }

        /// <summary>
        /// Reporte detallado de la investigación del problema.
        /// </summary>
        public string? InvestigationReport { get; set; }

        /// <summary>
        /// Fecha de cierre del reporte (opcional).
        /// </summary>
        /// <remarks>
        /// Solo se completa cuando el caso está resuelto.
        /// </remarks>
        [DataType(DataType.Date)]
        public DateTime? DateOfClose { get; set; }

        /// <summary>
        /// Indica si el problema impacta en las métricas PPM de la organización.
        /// </summary>
        [Range(0, double.MaxValue)]
        public bool ImpactoPPM { get; set; }

        /// <summary>
        /// Comentarios adicionales sobre el caso (opcional).
        /// </summary>
        public string? Comentarios { get; set; }

        /// <summary>
        /// Determina la responsabilidad asignada por el análisis de calidad.
        /// </summary>
        public ResponsabilidadOpciones Responsabilidad { get; set; }
    }
}