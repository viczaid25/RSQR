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
        Internal = 0,

        /// <summary>Reporte de problema en vehículo nuevo (0km)</summary>
        ZeroKm = 1,

        /// <summary>Reporte de problema en campo (vehículo en uso)</summary>
        Field = 2
    }

    /// <summary>
    /// Enumeración que representa las 5M's utilizadas en análisis de causa raíz.
    /// </summary>
    public enum CincoMOpciones
    {
        /// <summary>Problema relacionado con máquinas/equipos</summary>
        [Display(Name = "Machine")]
        Maquina,

        /// <summary>Problema relacionado con condiciones ambientales</summary>
        [Display(Name = "Environment")]
        MedioAmbiente,

        /// <summary>Problema relacionado con personal/operadores</summary>
        [Display(Name = "Workforce")]
        ManoDeObra,

        /// <summary>Problema relacionado con mediciones/calibraciones</summary>
        [Display(Name = "Measurement")]
        Medicion,

        /// <summary>Problema relacionado con materiales/materia prima</summary>
        [Display(Name = "Material")]
        Material,

        [Display(Name = "Method")]
        Method
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
        [Display(Name = "Supplier")]
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
        [Display(Name = "Date")]
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Nivel de prioridad/severidad del problema
        /// </summary>
        [Required(ErrorMessage = "You must select a value for the Problem Rank field.")]
        [RegularExpression("^(?!-Select-).*$", ErrorMessage = "You must select a valid option.")]
        [Display(Name = "Problem Rank")]
        public string? ProblemRank { get; set; }

        /// <summary>Nombre del usuario que creó el reporte</summary>
        [Display(Name = "User")]
        public string? UserName { get; set; }

        /// <summary>Línea o área donde se detectó el problema</summary>
        [Display(Name = "Line")]
        public string? Linea { get; set; }

        /// <summary>Tipo de reporte según clasificación</summary>
        [Display(Name = "Type")]
        public TipoReporte Tipo { get; set; }

        /// <summary>Título descriptivo del problema</summary>
        [Display(Name = "Problem Title")]
        public string? TituloProblema { get; set; }

        /// <summary>Categoría según metodología 5M's</summary>
        [Display(Name = "6 M's")]
        public CincoMOpciones CincoM { get; set; }

        /// <summary>Número de parte afectada</summary>
        [Display(Name = "Affected Part Number")]
        public string? NumParteAfectado { get; set; }

        /// <summary>Descripción general del problema</summary>
        [Display(Name = "Part Description")]
        public string? Descripcion { get; set; }

        /// <summary>Descripción detallada del problema</summary>
        [Display(Name = "Problem Description")]
        public string? DescripcionProblema { get; set; }

        // Campos de análisis (5W2H)
        /// <summary>Qué ocurrió (What)</summary>
        [Display(Name = "What problem was detected?")]
        public string? QueP { get; set; }

        /// <summary>Por qué ocurrió (Why)</summary>
        [Display(Name = "Why is it a problem?")]
        public string? PorqueP { get; set; }

        /// <summary>Dónde ocurrió (Where)</summary>
        [Display(Name = "Where was the problem detected?")]
        public string? DondeP { get; set; }

        /// <summary>Quién detectó (Who)</summary>
        [Display(Name = "Who detected the problem?")]
        public string? QuienP { get; set; }

        /// <summary>Cuándo ocurrió (When)</summary>
        [Display(Name = "When was the problem detected?")]
        public string? CuandoP { get; set; }

        /// <summary>Cuántos productos afectados (How many)</summary>
        [Display(Name = "How many pieces were detected with the problem?")]
        public int? CuantosP { get; set; }

        /// <summary>Cómo se detectó (How)</summary>
        [Display(Name = "How was the problem detected?")]
        public string? ComoP { get; set; }

        /// <summary>Lote o batch afectado</summary>
        [Display(Name = "Batch")]
        public string? Lote { get; set; }

        /// <summary>Número de reclamo en base de datos</summary>
        [Display(Name = "Customer Claim Number")]
        public int CustomerClaimNumber { get; set; }

        /// <summary>Número de reclamo formateado para visualización (no se persiste)</summary>
        [NotMapped]
        [Display(Name = "Número de Reclamo")]
        public string? CustomerClaimDisplayNumber { get; set; }

        /// <summary>Prefijo del cliente para formato de número (no se persiste)</summary>
        [NotMapped]
        [Display(Name = "Claim Prefix")]
        public string? CustomerClaimPrefix { get; set; }

        // Campos de acciones de contención
        /// <summary>Ítems para acciones de contención</summary>
        [Display(Name = "Items")]
        public List<string>? ContencionItems { get; set; } = new List<string>();

        /// <summary>Actividades de contención</summary>
        [Display(Name = "To Consider")]
        public List<string>? ContencionConsiderar { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        [Display(Name = "Contention Activity")]
        public List<string>? ContencionActivity { get; set; } = new List<string>();

        /// <summary>Fechas de inicio de acciones</summary>
        [Display(Name = "Contention Responsable")]
        public List<string>? ContencionResponsable { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        [Display(Name = "Total Suspecious Parts")]
        public List<string>? ContencionTotalSuspeciousParts { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        [Display(Name = "OK Parts")]
        public List<string>? ContencionOkParts { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        [Display(Name = "NG Parts")]
        public List<string>? ContencionNgParts { get; set; } = new List<string>();

        /// <summary>Responsables de las acciones</summary>
        [Display(Name = "% Effectiveness")]
        public List<int>? ContencionEffectiveness { get; set; } = new List<int>();



        /// <summary>Indica si se generó alerta de calidad</summary>
        [Display(Name = "Quality Alert")]
        public Boolean AlertaCalidad { get; set; }

        /// <summary>Disposición del material afectado</summary>
        [Display(Name = "Provision")]
        public string? Disposicion { get; set; }

        /// <summary>Indica si se realizó entrevista a involucrados</summary>
        [Display(Name = "Interview With Those Involved")]
        public Boolean EntrevistaInvolucrados { get; set; }

        /// <summary>Comentarios adicionales</summary>
        [Display(Name = "Comments")]
        public string? Comentarios { get; set; }

        // Campos de evaluación
        /// <summary>Nivel de severidad del problema</summary>
        [Display(Name = "Severity")]
        public string? Severidad { get; set; }

        /// <summary>Frecuencia de ocurrencia</summary>
        [Display(Name = "Occurrence")]
        public string? Ocurrencia { get; set; }

        /// <summary>Capacidad de detección</summary>
        [Display(Name = "Detection")]
        public string? Deteccion { get; set; }

        /// <summary>Acciones preventivas/No recurrencia</summary>
        [Display(Name = "AP NPR")]
        public string? AP_NPR { get; set; }

        /// <summary>Modo de falla identificado</summary>
        [Display(Name = "Failure Mode")]
        public string? ModoFalla { get; set; }

        /// <summary>Controles existentes al momento del problema</summary>
        [Display(Name = "Established Controls")]
        public string? ControlesEstablecidos { get; set; }

        /// <summary>Evidencia fotográfica del problema</summary>
        [Display(Name = "Photographic Evidence")]
        public List<byte[]>? EvidenciaFotografica { get; set; } = new List<byte[]>();

        // Campos adicionales
        /// <summary>Nombre del cliente afectado</summary>
        [Required(ErrorMessage = "El cliente es obligatorio")]
        [Display(Name = "Customer")]
        public string? Customer { get; set; }

        /// <summary>Fábrica de origen</summary>
        [Display(Name = "Mother Factory")]
        public string? MotherFactory { get; set; }

        /// <summary>Número de parte según cliente</summary>
        [Display(Name = "Customer Part Number")]
        public string? CustomerPartNumber { get; set; }

        /// <summary>Kilometraje/horas de uso (para field)</summary>
        [Display(Name = "Mileage")]
        public string? Mileage { get; set; }

        /// <summary>Reporte final de investigación</summary>
        [Display(Name = "Investigation Report")]
        public string? InvestigationReport { get; set; }

        /// <summary>Fecha de cierre del reporte</summary>
        [Display(Name = "Date Of Close")]
        public DateTime? DateOfClose { get; set; }

        /// <summary>Indica si impacta en métricas PPM</summary>
        [Display(Name = "Does it impact PPM?")]
        public bool ImpactoPPM { get; set; }

        /// <summary>Responsabilidad asignada</summary>
        public ResponsabilidadOpciones Responsabilidad { get; set; }

        public string? NombreCar { get; set; }

        // Propiedad de navegación para la relación 1:1
        public PpmReport? PpmReport { get; set; }

        [Display(Name = "Customer Report")]
        public string? CustomerReport { get; set; }  // Tipo string (o el tipo que necesites)

        //Failure Mode Identification
        [Display(Name = "Failure Mode")]
        public List<string>? FmMode { get; set; } = new List<string>();

        [Display(Name = "Process Name")]
        public List<string>? FmProcessName { get; set; } = new List<string>();

        [Display(Name = "6Ms")]
        public List<string>? Fm6Ms { get; set; } = new List<string>();

        [Display(Name = "Factor 1")]
        public List<string>? FmFactorUno { get; set; } = new List<string>();

        [Display(Name = "Factor 2")]
        public List<string>? FmFactorDos { get; set; } = new List<string>();

        [Display(Name = "Factor 3")]
        public List<string>? FmFactorTres { get; set; } = new List<string>();

        [Display(Name = "Related With The Issue?")]
        public List<string>? FmRelated { get; set; } = new List<string>();

        [Display(Name = "Contention Actions")]
        public List<string>? FmContentionActions { get; set; } = new List<string>();

        //Preventive Controls to Prevent FM - Currently Implemented
        [Display(Name = "Process Name")]
        public List<string>? PreventiveCProcessName { get; set; } = new List<string>();

        [Display(Name = "Manpower")]
        public List<string>? PreventiveCManpower { get; set; } = new List<string>();

        [Display(Name = "Method")]
        public List<string>? PreventiveCMethod { get; set; } = new List<string>();

        [Display(Name = "Machinary")]
        public List<string>? PreventiveCMachinary { get; set; } = new List<string>();

        [Display(Name = "Material")]
        public List<string>? PreventiveCMaterial { get; set; } = new List<string>();

        [Display(Name = "Measurement")]
        public List<string>? PreventiveCMeasurement { get; set; } = new List<string>();

        [Display(Name = "Environment")]
        public List<string>? PreventiveCEnvironment { get; set; } = new List<string>();

        [Display(Name = "Rank [O]")]
        public List<int>? PreventiveCRank { get; set; } = new List<int>();

        //Detection Controls to Prevent FM  - Currently Implemented
        [Display(Name = "Process Name")]
        public List<string>? DetectionCProcessName { get; set; } = new List<string>();

        [Display(Name = "Manpower")]
        public List<string>? DetectionCManpower { get; set; } = new List<string>();

        [Display(Name = "Method")]
        public List<string>? DetectionCMethod { get; set; } = new List<string>();

        [Display(Name = "Machinary")]
        public List<string>? DetectionCMachinary { get; set; } = new List<string>();

        [Display(Name = "Material")]
        public List<string>? DetectionCMaterial { get; set; } = new List<string>();

        [Display(Name = "Measurement")]
        public List<string>? DetectionCMeasurement { get; set; } = new List<string>();

        [Display(Name = "Environment")]
        public List<string>? DetectionCEnvironment { get; set; } = new List<string>();

        [Display(Name = "Rank [D]")]
        public List<int>? DetectionCRank { get; set; } = new List<int>();

        // Factores
        //Factor 1
        [Display(Name = "Factor")]
        public List<string>? FactorUno { get; set; } = new List<string>();

        [Display(Name = "1st Why")]
        public List<string>? FactorUnoPrimerWhy { get; set; } = new List<string>();

        [Display(Name = "2nd Why")]
        public List<string>? FactorUnoSegundoWhy { get; set; } = new List<string>();

        [Display(Name = "3rd Why")]
        public List<string>? FactorUnoTercerWhy { get; set; } = new List<string>();

        [Display(Name = "4th Why")]
        public List<string>? FactorUnoCuartoWhy { get; set; } = new List<string>();

        [Display(Name = "5th Why")]
        public List<string>? FactorUnoQuintoWhy { get; set; } = new List<string>();

        [Display(Name = "Corrective Actions")]
        public List<string>? FactorUnoCorrectiveActions { get; set; } = new List<string>();

        //Factor 2
        [Display(Name = "Factor")]
        public List<string>? FactorDos { get; set; } = new List<string>();

        [Display(Name = "1st Why")]
        public List<string>? FactorDosPrimerWhy { get; set; } = new List<string>();

        [Display(Name = "2nd Why")]
        public List<string>? FactorDosSegundoWhy { get; set; } = new List<string>();

        [Display(Name = "3rd Why")]
        public List<string>? FactorDosTercerWhy { get; set; } = new List<string>();

        [Display(Name = "4th Why")]
        public List<string>? FactorDosCuartoWhy { get; set; } = new List<string>();

        [Display(Name = "5th Why")]
        public List<string>? FactorDosQuintoWhy { get; set; } = new List<string>();

        [Display(Name = "Corrective Actions")]
        public List<string>? FactorDosCorrectiveActions { get; set; } = new List<string>();

        //Factor 3
        [Display(Name = "Factor")]
        public List<string>? FactorTres { get; set; } = new List<string>();

        [Display(Name = "1st Why")]
        public List<string>? FactorTresPrimerWhy { get; set; } = new List<string>();

        [Display(Name = "2nd Why")]
        public List<string>? FactorTresSegundoWhy { get; set; } = new List<string>();

        [Display(Name = "3rd Why")]
        public List<string>? FactorTresTercerWhy { get; set; } = new List<string>();

        [Display(Name = "4th Why")]
        public List<string>? FactorTresCuartoWhy { get; set; } = new List<string>();

        [Display(Name = "5th Why")]
        public List<string>? FactorTresQuintoWhy { get; set; } = new List<string>();

        [Display(Name = "Corrective Actions")]
        public List<string>? FactorTresCorrectiveActions { get; set; } = new List<string>();

        [Display(Name = "Was The Defect Duplicated?")]
        public string? DefectoDuplicado { get; set; }


        //Preventive Controls to Prevent FM - Proposed Corrective Action Plan
        [Display(Name = "Process Name")]
        public List<string>? PreventiveCProcessNameC { get; set; } = new List<string>();

        [Display(Name = "Manpower")]
        public List<string>? PreventiveCManpowerC { get; set; } = new List<string>();

        [Display(Name = "Method")]
        public List<string>? PreventiveCMethodC { get; set; } = new List<string>();

        [Display(Name = "Machinary")]
        public List<string>? PreventiveCMachinaryC { get; set; } = new List<string>();

        [Display(Name = "Material")]
        public List<string>? PreventiveCMaterialC { get; set; } = new List<string>();

        [Display(Name = "Measurement")]
        public List<string>? PreventiveCMeasurementC { get; set; } = new List<string>();

        [Display(Name = "Environment")]
        public List<string>? PreventiveCEnvironmentC { get; set; } = new List<string>();

        [Display(Name = "Rank [O]")]
        public List<int>? PreventiveCRankC { get; set; } = new List<int>();

        //Detection Controls to Prevent FM  - Proposed Corrective Action Plan
        [Display(Name = "Process Name")]
        public List<string>? DetectionCProcessNameC { get; set; } = new List<string>();

        [Display(Name = "Manpower")]
        public List<string>? DetectionCManpowerC { get; set; } = new List<string>();

        [Display(Name = "Method")]
        public List<string>? DetectionCMethodC { get; set; } = new List<string>();

        [Display(Name = "Machinary")]
        public List<string>? DetectionCMachinaryC { get; set; } = new List<string>();

        [Display(Name = "Material")]
        public List<string>? DetectionCMaterialC { get; set; } = new List<string>();

        [Display(Name = "Measurement")]
        public List<string>? DetectionCMeasurementC { get; set; } = new List<string>();

        [Display(Name = "Environment")]
        public List<string>? DetectionCEnvironmentC { get; set; } = new List<string>();

        [Display(Name = "Rank [D]")]
        public List<int>? DetectionCRankC { get; set; } = new List<int>();

        //D5 Permanent Corrective Actions
        //Occurrence
        [Display(Name = "No Item")]
        public List<string>? OccurrenceItems { get; set; } = new List<string>();

        [Display(Name = "Action")]
        public List<string>? OccurrenceAction { get; set; } = new List<string>();

        [Display(Name = "Responsable")]
        public List<string>? OccurrenceResponsable { get; set; } = new List<string>();

        [Display(Name = "Department")]
        public List<string>? OccurrenceDepartment { get; set; } = new List<string>();

        [Display(Name = "Opening Date")]
        public List<DateTime>? OccurrenceOpeningDate { get; set; } = new List<DateTime>();

        [Display(Name = "Close Date")]
        public List<DateTime>? OccurrenceCloseDate { get; set; } = new List<DateTime>();

        [Display(Name = "AMEF")]
        public List<string>? OccurrenceAmef { get; set; } = new List<string>();

        [Display(Name = "CP")]
        public List<string>? OccurrenceCp { get; set; } = new List<string>();

        //Detection
        [Display(Name = "No Item")]
        public List<string>? DetectionItems { get; set; } = new List<string>();

        [Display(Name = "Action")]
        public List<string>? DetectionAction { get; set; } = new List<string>();

        [Display(Name = "Responsable")]
        public List<string>? DetectionResponsable { get; set; } = new List<string>();

        [Display(Name = "Department")]
        public List<string>? DetectionDepartment { get; set; } = new List<string>();

        [Display(Name = "Opening Date")]
        public List<DateTime>? DetectionOpeningDate { get; set; } = new List<DateTime>();

        [Display(Name = "Close Date")]
        public List<DateTime>? DetectionCloseDate { get; set; } = new List<DateTime>();

        [Display(Name = "AMEF")]
        public List<string>? DetectionAmef { get; set; } = new List<string>();

        [Display(Name = "CP")]
        public List<string>? DetectionCp { get; set; } = new List<string>();

        //D6 Verification of Implementation of Permanent Corrective Actions
        //Occurrence
        [Display(Name = "No Item")]
        public List<string>? VerOccurrenceItems { get; set; } = new List<string>();

        [Display(Name = "Action")]
        public List<string>? VerOccurrenceAction { get; set; } = new List<string>();

        [Display(Name = "Responsable")]
        public List<string>? VerOccurrenceResponsable { get; set; } = new List<string>();

        [Display(Name = "Department")]
        public List<string>? VerOccurrenceDepartment { get; set; } = new List<string>();

        [Display(Name = "Opening Date")]
        public List<DateTime>? VerOccurrenceOpeningDate { get; set; } = new List<DateTime>();

        [Display(Name = "Close Date")]
        public List<DateTime>? VerOccurrenceCloseDate { get; set; } = new List<DateTime>();

        [Display(Name = "AMEF")]
        public List<string>? VerOccurrenceAmef { get; set; } = new List<string>();

        [Display(Name = "CP")]
        public List<string>? VerOccurrenceCp { get; set; } = new List<string>();

        //Detection
        [Display(Name = "No Item")]
        public List<string>? VerDetectionItems { get; set; } = new List<string>();

        [Display(Name = "Action")]
        public List<string>? VerDetectionAction { get; set; } = new List<string>();

        [Display(Name = "Responsable")]
        public List<string>? VerDetectionResponsable { get; set; } = new List<string>();

        [Display(Name = "Department")]
        public List<string>? VerDetectionDepartment { get; set; } = new List<string>();

        [Display(Name = "Opening Date")]
        public List<DateTime>? VerDetectionOpeningDate { get; set; } = new List<DateTime>();

        [Display(Name = "Close Date")]
        public List<DateTime>? VerDetectionCloseDate { get; set; } = new List<DateTime>();

        [Display(Name = "AMEF")]
        public List<string>? VerDetectionAmef { get; set; } = new List<string>();

        [Display(Name = "CP")]
        public List<string>? VerDetectionCp { get; set; } = new List<string>();

        //D7 D8 Preventive Activities And Standards of Ensure The Horizontality of countermeasures

        [Display(Name = "Documentation")]
        public List<string>? D7D8Documentation { get; set; } = new List<string>();

        [Display(Name = "SN")]
        public List<string>? D7D8Sn { get; set; } = new List<string>();

        [Display(Name = "Code/Document Description")]
        public List<string>? D7D8CodeDescription { get; set; } = new List<string>();

        [Display(Name = "Responsible")]
        public List<string>? D7D8Responsible { get; set; } = new List<string>();

        [Display(Name = "Deadline")]
        public List<DateTime>? D7D8Deadline { get; set; } = new List<DateTime>();

        [Display(Name = "Actual Closing Date")]
        public List<DateTime>? D7D8ActualClosingDate { get; set; } = new List<DateTime>();

        [Display(Name = "Comments")]
        public List<string>? D7D8Comments { get; set; } = new List<string>();

        [Display(Name = "Approval")]
        public string? Approval { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }
    }
}