using System.ComponentModel.DataAnnotations;

namespace RSQR.Models
{
    public enum TipoReporte
    {
        Interno = 0,
        CeroKm = 1,
        Field = 2
    }

    public enum CincoMOpciones
    {
        [Display(Name = "Maquina/Machine")]
        Maquina,

        [Display(Name = "Medio ambiente/Environment")]
        MedioAmbiente,

        [Display(Name = "Mano de obra/Workforce")]
        ManoDeObra,

        [Display(Name = "Medición/Measurement")]
        Medicion,

        [Display(Name = "Material")]
        Material
    }

    public enum ResponsabilidadOpciones
    {
        [Display(Name = "MEAX")]
        Meax,

        [Display(Name = "Proveedor")]
        Proveedor
    }
    public class Reporte
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un valor para el campo ProblemRank.")]
        [RegularExpression("^(?!-Seleccionar-).*$", ErrorMessage = "Debe seleccionar una opción válida.")]
        public string? ProblemRank { get; set; }
        public string? UserName { get; set; }
        public string? Linea { get; set; }
        public TipoReporte Tipo { get; set; }
        public string? TituloProblema { get; set; }
        public CincoMOpciones CincoM { get; set; }
        public string? NumParteAfectado { get; set; }
        public string? Descripcion { get; set; }
        public string? DescripcionProblema { get; set; }
        public string? QueP { get; set; }
        public string? PorqueP { get; set; }
        public string? DondeP { get; set; }
        public string? QuienP { get; set; }
        public string? CuandoP { get; set; }
        public string? CuantosP { get; set; }
        public string? ComoP { get; set; }
        public string? Lote { get; set; }
        public int CustomerClaimNumber { get; set; }
        //public string? PreguntasAdicionales { get; set; }

        // Listas para los campos de Contención
        public List<string>? ContencionItems { get; set; } = new List<string>();
        public List<string>? ContencionActividades { get; set; } = new List<string>();
        public List<string>? ContencionResponsables { get; set; } = new List<string>();
        public List<DateTime>? ContencionFechasInicio { get; set; } = new List<DateTime>();

        public Boolean AlertaCalidad { get; set; }
        public string? Disposicion { get; set; }
        public Boolean EntrevistaInvolucrados { get; set; }
        public string? Comentarios { get; set; }
        public string? Severidad { get; set; }
        public string? Ocurrencia { get; set; }
        public string? Deteccion { get; set; }
        public string? AP_NPR { get; set; }
        public string? ModoFalla { get; set; }
        public string? ControlesEstablecidos { get; set; }
        public List<byte[]>? EvidenciaFotografica { get; set; }
        public string? Customer { get; set; }
        public string? MotherFactory { get; set; }
        public string? CustomerPartNumber { get; set; }
        public string? Mileage { get; set; }
        public string? InvestigationReport { get; set; }
        public DateTime? DateOfClose { get; set; }
        public bool ImpactoPPM { get; set; }
        public ResponsabilidadOpciones Responsabilidad { get; set; }


    }
}
