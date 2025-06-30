using System;
using System.ComponentModel.DataAnnotations;

namespace RSQR.Models
{
    /// <summary>
    /// Representa un consecutivo numérico para archivos, asociado a unidades de negocio.
    /// </summary>
    /// <remarks>
    /// Esta entidad se utiliza para generar números consecutivos únicos por combinación
    /// de unidad de negocio, código de negocio y año.
    /// </remarks>
    public class ConsecutivoArchivo
    {
        /// <summary>
        /// Identificador único del registro en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la unidad de negocio asociada.
        /// </summary>
        /// <remarks>
        /// Requerido, con longitud máxima de 50 caracteres.
        /// </remarks>a
        [Required, StringLength(50)]
        public string UnidadNegocio { get; set; }

        /// <summary>
        /// Código identificador del negocio.
        /// </summary>
        /// <remarks>
        /// Requerido, con longitud máxima de 10 caracteres.
        /// </remarks>
        [Required, StringLength(10)]
        public string CodigoNegocio { get; set; }

        /// <summary>
        /// Año al que pertenece el consecutivo.
        /// </summary>
        [Required]
        public int Anio { get; set; }

        /// <summary>
        /// Número consecutivo actual.
        /// </summary>
        /// <remarks>
        /// Valor por defecto: 1. Se incrementa automáticamente con cada nuevo uso.
        /// </remarks>
        [Required]
        public int Consecutivo { get; set; } = 1; // Valor por defecto

        /// <summary>
        /// Fecha y hora de la última actualización del registro.
        /// </summary>
        /// <remarks>
        /// Se establece automáticamente a la fecha/hora actual al crearse o modificarse.
        /// </remarks>
        public DateTime FechaActualizacion { get; set; } = DateTime.Now;
    }
}