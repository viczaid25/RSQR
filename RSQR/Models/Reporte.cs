using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSQR.Models
{
    /// <summary>
    /// Enumeración que define los tipos de reportes disponibles en el sistema.
    /// </summary>
    public enum TipoReporte
    {
        /// <summary>Reporte interno de calidad</summary>
        Interno = 0,

        /// <summary>Reporte de problema en vehículo nuevo (0km)</summary>
        CeroKm = 1,

        /// <summary>Reporte de problema en campo (vehículo en uso)</summary>
        Field = 2
    }

    /// <summary>
    /// Enumeración que representa las 5M's utilizadas en análisis de causa raíz.
    /// </summary>
    public enum CincoMOpciones
    {
        /// <summary>Problema relacionado con máquinas/equipos</summary>
        [Display(Name = "Maquina/Machine")]
        Maquina,

        /// <summary>Problema relacionado con condiciones ambientales</summary>
        [Display(Name = "Medio ambiente/Environment")]
        MedioAmbiente,

        /// <summary>Problema relacionado con personal/operadores</summary>
        [Display(Name = "Mano de obra/Workforce")]
        ManoDeObra,

        /// <summary>Problema relacionado con mediciones/calibraciones</summary>
        [Display(Name = "Medición/Measurement")]
        Medicion,

        /// <summary>Problema relacionado con materiales/materia prima</summary>
        [Display(Name = "Material")]
        Material
    }

    /// <summary>
    /// Enumeración que define las posibles responsabilidades en un reporte.
    /// </summary>
    public enum ResponsabilidadOpciones
    {
        /// <summary>Responsabilidad asignada a MEAX</summary>
        [Display(Name = "MEAX")]
        Meax,

        /// <summary>Responsabilidad asignada a proveedor externo</summary>
        [Display(Name = "Proveedor")]
        Proveedor
    }

    /// <summary>
    /// Modelo principal que representa un reporte de calidad en el sistema.
    /// </summary>
    /// <remarks>
    /// Contiene toda la información relacionada con un problema de calidad reportado,
    /// incluyendo análisis, acciones de contención y seguimiento.
    /// </remarks>
    public class Reporte
    {
        /// <summary>Identificador único del reporte</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Fecha de creación del reporte</summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Nivel de prioridad/severidad del problema
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar un valor para el campo ProblemRank.")]
        [RegularExpression("^(?!-Seleccionar-).*$", ErrorMessage = "Debe seleccionar una opción válida.")]
        public string? ProblemRank { get; set; }

        /// <summary>Nombre del usuario que creó el reporte</summary>
        public string? UserName { get; set; }

        /// <summary>Línea o área donde se detectó el problema</summary>
        public string? Linea { get; set; }

        /// <summary>Tipo de reporte según clasificación</summary>
        public TipoReporte Tipo { get; set; }

        /// <summary>Título descriptivo del problema</summary>
        public string? TituloProblema { get; set; }

        /// <summary>Categoría según metodología 5M's</summary>
        public CincoMOpciones CincoM { get; set; }

        /// <summary>Número de parte afectada</summary>
        public string? NumParteAfectado { get; set; }

        /// <summary>Descripción general del problema</summary>
        public string? Descripcion { get; set; }

        /// <summary>Descripción detallada del problema</summary>
        public string? DescripcionProblema { get; set; }

        // Campos de análisis (5W2H)
        /// <summary>Qué ocurrió (What)</summary>
        [Display(Name = "Que Problema fue detectado?")]
        public string? QueP { get; set; }

        /// <summary>Por qué ocurrió (Why)</summary>
        [Display(Name = "Porque es un problema?")]
        public string? PorqueP { get; set; }

        /// <summary>Dónde ocurrió (Where)</summary>
        [Display(Name = "Donde se detecto el problema?")]
        public string? DondeP { get; set; }

        /// <summary>Quién detectó (Who)</summary>
        [Display(Name = "Quien detecto el problema?")]
        public string? QuienP { get; set; }

        /// <summary>Cuándo ocurrió (When)</summary>
        [Display(Name = "Cuando fue detectado el problema?")]
        public string? CuandoP { get; set; }

        /// <summary>Cuántos productos afectados (How many)</summary>
        [Display(Name = "Cuantas piezas fueron detectadas con el problema?")]
        public int? CuantosP { get; set; }

        /// <summary>Cómo se detectó (How)</summary>
        [Display(Name = "Como se detecto el problema?")]
        public string? ComoP { get; set; }

        /// <summary>Lote o batch afectado</summary>
        public string? Lote { get; set; }

        /// <summary>Número de reclamo en base de datos</summary>
        [Display(Name = "Número de Reclamo")]
        public int CustomerClaimNumber { get; set; }

        /// <summary>Número de reclamo formateado para visualización (no se persiste)</summary>
        [NotMapped]
        [Display(Name = "Número de Reclamo")]
        public string? CustomerClaimDisplayNumber { get; set; }

        /// <summary>Prefijo del cliente para formato de número (no se persiste)</summary>
        [NotMapped]
        public string? CustomerClaimPrefix { get; set; }

        // Campos de acciones de contención
        /// <summary>Ítems para acciones de contención</summary>
        public List<string>? ContencionItems { get; set; } = new List<string>();

        /// <summary>Actividades de contención</summary>
        public List<string>? ContencionActividades { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        public List<string>? ContencionResponsables { get; set; } = new List<string>();

        /// <summary>Fechas de inicio de acciones</summary>
        public List<DateTime>? ContencionFechasInicio { get; set; } = new List<DateTime>();

        /// <summary>Indica si se generó alerta de calidad</summary>
        public Boolean AlertaCalidad { get; set; }

        /// <summary>Disposición del material afectado</summary>
        public string? Disposicion { get; set; }

        /// <summary>Indica si se realizó entrevista a involucrados</summary>
        public Boolean EntrevistaInvolucrados { get; set; }

        /// <summary>Comentarios adicionales</summary>
        public string? Comentarios { get; set; }

        // Campos de evaluación
        /// <summary>Nivel de severidad del problema</summary>
        public string? Severidad { get; set; }

        /// <summary>Frecuencia de ocurrencia</summary>
        public string? Ocurrencia { get; set; }

        /// <summary>Capacidad de detección</summary>
        public string? Deteccion { get; set; }

        /// <summary>Acciones preventivas/No recurrencia</summary>
        public string? AP_NPR { get; set; }

        /// <summary>Modo de falla identificado</summary>
        public string? ModoFalla { get; set; }

        /// <summary>Controles existentes al momento del problema</summary>
        public string? ControlesEstablecidos { get; set; }

        /// <summary>Evidencia fotográfica del problema</summary>
        public List<byte[]>? EvidenciaFotografica { get; set; }

        // Campos adicionales
        /// <summary>Nombre del cliente afectado</summary>
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public string? Customer { get; set; }

        /// <summary>Fábrica de origen</summary>
        public string? MotherFactory { get; set; }

        /// <summary>Número de parte según cliente</summary>
        public string? CustomerPartNumber { get; set; }

        /// <summary>Kilometraje/horas de uso (para field)</summary>
        public string? Mileage { get; set; }

        /// <summary>Reporte final de investigación</summary>
        public string? InvestigationReport { get; set; }

        /// <summary>Fecha de cierre del reporte</summary>
        public DateTime? DateOfClose { get; set; }

        /// <summary>Indica si impacta en métricas PPM</summary>
        public bool ImpactoPPM { get; set; }

        /// <summary>Responsabilidad asignada</summary>
        public ResponsabilidadOpciones Responsabilidad { get; set; }

        public string? NombreCar { get; set; }
    }
}