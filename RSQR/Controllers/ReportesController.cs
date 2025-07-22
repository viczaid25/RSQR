using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RSQR.Data;
using RSQR.Models;
using RSQR.Services;
using RSQR.Utilities;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace RSQR.Controllers
{
    /// <summary>
    /// Controlador para gestionar los reportes del sistema.
    /// </summary>
    /// <remarks>
    /// Este controlador está protegido con autorización, por lo que solo los usuarios autenticados pueden acceder a sus acciones.
    /// </remarks>
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReportesController> _logger;

        /// <summary>
        /// Constructor del controlador ReportesController.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        public ReportesController(ApplicationDbContext context, IEmailService emailService, ILogger<ReportesController> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Muestra la lista de todos los reportes.
        /// </summary>
        /// <returns>Una vista con la lista de reportes.</returns>
        // GET: Reportes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reportes.ToListAsync());
        }

        /// <summary>
        /// Muestra los detalles de un reporte específico.
        /// </summary>
        /// <param name="id">El ID del reporte.</param>
        /// <returns>Una vista con los detalles del reporte.</returns>
        // GET: Reportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        /// <summary>
        /// Muestra el formulario para crear un nuevo reporte.
        /// </summary>
        /// <returns>Una vista con el formulario de creación.</returns>
        // GET: Reportes/Create
        public IActionResult Create()
        {

            var displayName = User.FindFirst("DisplayName")?.Value; // Obtiene el DisplayName desde las claims

            var reporte = new Reporte
            {
                UserName = displayName // Almacena el username en el modelo
            };

            ViewBag.DisplayName = displayName; // Enviar DisplayName a la vista

            ViewBag.ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Select-", Selected = true },  // Opción por defecto
                new SelectListItem { Value = "Bajo", Text = "Low" },
                new SelectListItem { Value = "Medio", Text = "Medium" },
                new SelectListItem { Value = "Alto", Text = "High" }
            };

            // Nuevo: Opciones para Linea
            ViewBag.LineaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Select-", Selected = true },  // Opción por defecto
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                new SelectListItem { Value = "ALTERNATOR", Text = "ALTERNATOR" },
                new SelectListItem { Value = "EPS (3G)", Text = "EPS (3G)" },
                new SelectListItem { Value = "PT EPS", Text = "PT EPS" },
                new SelectListItem { Value = "PT SSU", Text = "PT SSU" },
                new SelectListItem { Value = "PT FOB", Text = "PT FOB" },
                new SelectListItem { Value = "PT RCV", Text = "PT RCV" },
                new SelectListItem { Value = "PT BCM", Text = "PT BCM" },
                new SelectListItem { Value = "PT LFU", Text = "PT LFU" },
                new SelectListItem { Value = "EPS", Text = "EPS" },
                new SelectListItem { Value = "CM", Text = "CM" },
                new SelectListItem { Value = "LCM", Text = "LCM" },
                new SelectListItem { Value = "AMP", Text = "AMP" },
                new SelectListItem { Value = "R1", Text = "R1" },
                new SelectListItem { Value = "CID", Text = "CID" },
                new SelectListItem { Value = "PT CM", Text = "PT CM" }
            };

            return View();
        }

        /// <summary>
        /// Procesa el formulario de creación de un nuevo reporte.
        /// </summary>
        /// <param name="reporte">El objeto Reporte con los datos del formulario.</param>
        /// <param name="EvidenciaFotografica">Lista de archivos de evidencia fotográfica.</param>
        /// <returns>Redirige a la lista de reportes si el modelo es válido; de lo contrario, muestra el formulario con errores.</returns>
        // POST: Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado,Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP,Lote,CustomerClaimNumber,ContencionItems,ContencionActividades,ContencionResponsables,ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios,Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,EvidenciaFotografica,Customer,MotherFactory,CustomerPartNumber,Mileage,InvestigationReport,DateOfClose,ImpactoPPM,Responsabilidad,NombreCar,CustomerReport")] Reporte reporte, List<IFormFile>? EvidenciaFotografica)
        {
            if (ModelState.IsValid)
            {
                // Procesar la lista de archivos
                if (EvidenciaFotografica != null && EvidenciaFotografica.Count > 0)
                {
                    reporte.EvidenciaFotografica = new List<byte[]>();
                    foreach (var file in EvidenciaFotografica)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);
                                reporte.EvidenciaFotografica.Add(ms.ToArray());
                            }
                        }
                    }
                }

                // Validar número de reclamo
                if (reporte.CustomerClaimNumber <= 0)
                {
                    reporte.CustomerClaimNumber = await GetNextClaimNumberForCustomer(reporte.Customer);
                }

                // Generar nombre del archivo CAR antes de guardar
                string codigoNegocio = BusinessUnitMapping.GetBusinessUnitCode(reporte.Linea ?? "DEFAULT");
                int anioActual = DateTime.Now.Year % 100;
                int consecutivo = ObtenerSiguienteConsecutivo(codigoNegocio, DateTime.Now.Year);
                reporte.NombreCar = $"CAR-{codigoNegocio}-{consecutivo:D2}-{anioActual:D2}.xlsx";

                _context.Add(reporte);
                await _context.SaveChangesAsync();

                // Mapeo de líneas a destinatarios
                var lineaDestinatarios = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["STARTER"] = "rsqtest@meax.mx",
                    ["ALTERNATOR"] = "rsqtest@meax.mx",
                    ["EPS (3G)"] = "rsqtest@meax.mx",
                    ["PT EPS"] = "rsqtest@meax.mx",
                    ["PT SSU"] = "rsqtest@meax.mx",
                    ["PT FOB"] = "rsqtest@meax.mx",
                    ["PT RCV"] = "rsqtest@meax.mx",
                    ["PT BCM"] = "rsqtest@meax.mx",
                    ["PT LFU"] = "rsqtest@meax.mx",
                    ["EPS"] = "rsqtest@meax.mx  ",
                    ["CM"] = "rsqtest@meax.mx",
                    ["LCM"] = "rsqtest@meax.mx",
                    ["AMP"] = "rsqtest@meax.mx",
                    ["R1"] = "rsqtest@meax.mx",
                    ["CID"] = "rsqtest@meax.mx",        
                    ["PT CM"] = "rsqtest@meax.mx",
                    ["DEFAULT"] = "rsqtest@meax.mx" // Valor por defecto
                };

                // Determinar destinatario según responsabilidad y línea
                string destinatario;
                switch (reporte.Responsabilidad)
                {
                    case ResponsabilidadOpciones.Meax:
                        destinatario = lineaDestinatarios.TryGetValue(reporte.Linea ?? "", out var email)
                                    ? email
                                    : lineaDestinatarios["DEFAULT"];
                        break;

                    case ResponsabilidadOpciones.Proveedor:
                        destinatario = "Akaren.Gonzalez@meax.mx";
                        break;

                    default:
                        destinatario = "Sofia.Ramirez@meax.mx";
                        break;
                }

                // Construir cuerpo del correo
                string asunto = "Immediate Attention required: A Customer Report Has Been Issued";
                string cuerpo = $@"Dear Team,

                                Please be advised that a {reporte.Tipo.ToString()} report has been received from customer ""{reporte.Customer}"" regarding the issue ""{reporte.TituloProblema}"". The information of the issue is now contained in CAR ({reporte.Id}).

                                Kindly review the information and proceed accordingly.

                                Best Regards.";

                await _emailService.SendEmailAsync(destinatario, asunto, cuerpo, "rsqtest@meax.mx");

                // Manejo de PPM si es necesario
                if (reporte.ImpactoPPM)
                {
                    var ppmReport = new PpmReport
                    {
                        Fecha = reporte.Fecha,
                        Customer = reporte.Customer!,
                        MotherFactory = reporte.MotherFactory!,
                        CustomerPartNumber = reporte.CustomerPartNumber!,
                        Mileage = reporte.Mileage!,
                        InvestigationReport = reporte.InvestigationReport!,
                        DateOfClose = reporte.DateOfClose,
                        ImpactoPPM = reporte.ImpactoPPM,
                        Responsabilidad = reporte.Responsabilidad,
                        Comentarios = reporte.Comentarios,
                        CuantosP = reporte.CuantosP,
                        CustomerClaimNumber = reporte.CustomerClaimNumber,
                        Linea = reporte.Linea,
                        Lote = reporte.Lote,
                        NumParteAfectado = reporte.NumParteAfectado,
                        Tipo = reporte.Tipo,
                        TituloProblema = reporte.TituloProblema
                    };
                    _context.Add(ppmReport);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Campo: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View(reporte);
        }

        private async Task<int> GetNextClaimNumberForCustomer(string customer)
        {
            if (string.IsNullOrEmpty(customer))
                return 1; // Valor por defecto si no hay cliente

            var lastNumber = await _context.Reportes
                .Where(r => r.Customer == customer)
                .MaxAsync(r => (int?)r.CustomerClaimNumber) ?? 0;

            return lastNumber + 1;
        }



        /// <summary>
        /// Muestra el formulario para editar un reporte existente.
        /// </summary>
        /// <param name="id">El ID del reporte a editar.</param>
        /// <returns>Una vista con el formulario de edición.</returns>
        // GET: Reportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Seleccionar-", Selected = true },  // Opción por defecto
                new SelectListItem { Value = "Bajo", Text = "Bajo" },
                new SelectListItem { Value = "Medio", Text = "Medio" },
                new SelectListItem { Value = "Alto", Text = "Alto" }
            };

            // Nuevo: Opciones para Linea
            ViewBag.LineaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                new SelectListItem { Value = "ALTERNATOR", Text = "ALTERNATOR" },
                new SelectListItem { Value = "SSU Circuit Board", Text = "SSU Circuit Board" },
                new SelectListItem { Value = "FOB", Text = "FOB" },
                new SelectListItem { Value = "RCV", Text = "RCV" },
                new SelectListItem { Value = "CM", Text = "CM" },
                new SelectListItem { Value = "EPS(3G)", Text = "EPS(3G)" },
                new SelectListItem { Value = "PT BCM", Text = "PT BCM" },
                new SelectListItem { Value = "PT LFU", Text = "PT LFU" },
                new SelectListItem { Value = "EPS", Text = "EPS" },
                new SelectListItem { Value = "CID", Text = "CID" },
                new SelectListItem { Value = "LCM", Text = "LCM" },
                new SelectListItem { Value = "AMP", Text = "AMP" },
                new SelectListItem { Value = "R1", Text = "R1" },
                new SelectListItem { Value = "PT CM", Text = "PT CM" },
                new SelectListItem { Value = "PT DISPLAY", Text = "PT DISPLAY" }
            };

            if (id == null) return NotFound();
            

            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null) return NotFound();

            var archivosExistentes = new List<object>();
            if (reporte.EvidenciaFotografica != null)
            {
                for (int i = 0; i < reporte.EvidenciaFotografica.Count; i++)
                {
                    archivosExistentes.Add(new { Index = i, Data = reporte.EvidenciaFotografica[i] });
                }
            }

            ViewBag.ArchivosExistentes = archivosExistentes;

            return View(reporte);
        }

        /// <summary>
        /// Procesa el formulario de edición de un reporte existente.
        /// </summary>
        /// <param name="id">El ID del reporte a editar.</param>
        /// <param name="reporte">El objeto Reporte con los datos actualizados.</param>
        /// <returns>Redirige a la lista de reportes si el modelo es válido; de lo contrario, muestra el formulario con errores.</returns>
        // POST: Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
    [Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado," +
          "Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP," +
          "Lote,CustomerClaimNumber,ContencionItems,ContencionActividades,ContencionResponsables," +
          "ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios," +
          "Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,Customer," +
          "MotherFactory,CustomerPartNumber,Mileage,InvestigationReport,DateOfClose,ImpactoPPM," +
          "Responsabilidad,NombreCar,CustomerReport,FmMode,FmProcessName,Fm6Ms,FmFactorUno,FmFactorDos," +
          "FmFactorTres,FmRelated,FmContentionActions,PreventiveCProcessName,PreventiveCManpower," +
          "PreventiveCMethod,PreventiveCMachinary,PreventiveCMaterial,PreventiveCMeasurement," +
          "PreventiveCEnvironment,PreventiveCRank,DetectionCProcessName,DetectionCManpower," +
          "DetectionCMethod,DetectionCMachinary,DetectionCMaterial,DetectionCMeasurement," +
          "DetectionCEnvironment,DetectionCRank,FactorUno,FactorUnoPrimerWhy,FactorUnoSegundoWhy," +
          "FactorUnoTercerWhy,FactorUnoCuartoWhy,FactorUnoQuintoWhy,FactorUnoCorrectiveActions," +
          "FactorDos,FactorDosPrimerWhy,FactorDosSegundoWhy,FactorDosTercerWhy,FactorDosCuartoWhy," +
          "FactorDosQuintoWhy,FactorDosCorrectiveActions,FactorTres,FactorTresPrimerWhy," +
          "FactorTresSegundoWhy,FactorTresTercerWhy,FactorTresCuartoWhy,FactorTresQuintoWhy," +
          "FactorTresCorrectiveActions,OccurrenceItems,OccurrenceAction,OccurrenceResponsable," +
          "OccurrenceDepartment,OccurrenceOpeningDate,OccurrenceCloseDate,OccurrenceAmef,OccurrenceCp," +
          "DetectionItems,DetectionAction,DetectionResponsable,DetectionDepartment,DetectionOpeningDate," +
          "DetectionCloseDate,DetectionAmef,DetectionCp")]
    Reporte reporte,
    List<IFormFile>? EvidenciaFotografica, List<int>? ArchivosAMantener)
        {
            if (id != reporte.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Cargar la entidad existente incluyendo el PpmReport relacionado
                    var existingReport = await _context.Reportes
                        .Include(r => r.PpmReport)
                        .FirstOrDefaultAsync(r => r.Id == id);

                    if (existingReport == null) return NotFound();

                    // Verificar si hubo cambios importantes que requieran notificación
                    bool requiereNotificacion = RequiereNotificacion(existingReport, reporte);

                    // Copiar los valores del modelo recibido a la entidad existente
                    var entry = _context.Entry(existingReport);
                    entry.CurrentValues.SetValues(reporte);
                    entry.Property(e => e.EvidenciaFotografica).IsModified = false;

                    // 1. Actualizar las listas de Failure Mode
                    existingReport.FmMode = reporte.FmMode ?? new List<string>();
                    existingReport.FmProcessName = reporte.FmProcessName ?? new List<string>();
                    existingReport.Fm6Ms = reporte.Fm6Ms ?? new List<string>();
                    existingReport.FmFactorUno = reporte.FmFactorUno ?? new List<string>();
                    existingReport.FmFactorDos = reporte.FmFactorDos ?? new List<string>();
                    existingReport.FmFactorTres = reporte.FmFactorTres ?? new List<string>();
                    existingReport.FmRelated = reporte.FmRelated ?? new List<string>();
                    existingReport.FmContentionActions = reporte.FmContentionActions ?? new List<string>();

                    // 2. Actualizar las listas de Preventive Actions
                    existingReport.PreventiveCProcessName = reporte.PreventiveCProcessName ?? new List<string>();
                    existingReport.PreventiveCManpower = reporte.PreventiveCManpower ?? new List<string>();
                    existingReport.PreventiveCMethod = reporte.PreventiveCMethod ?? new List<string>();
                    existingReport.PreventiveCMachinary = reporte.PreventiveCMachinary ?? new List<string>();
                    existingReport.PreventiveCMaterial = reporte.PreventiveCMaterial ?? new List<string>();
                    existingReport.PreventiveCMeasurement = reporte.PreventiveCMeasurement ?? new List<string>();
                    existingReport.PreventiveCEnvironment = reporte.PreventiveCEnvironment ?? new List<string>();
                    existingReport.PreventiveCRank = reporte.PreventiveCRank ?? new List<int>();

                    // 3. Actualizar las listas de Detection Controls
                    existingReport.DetectionCProcessName = reporte.DetectionCProcessName ?? new List<string>();
                    existingReport.DetectionCManpower = reporte.DetectionCManpower ?? new List<string>();
                    existingReport.DetectionCMethod = reporte.DetectionCMethod ?? new List<string>();
                    existingReport.DetectionCMachinary = reporte.DetectionCMachinary ?? new List<string>();
                    existingReport.DetectionCMaterial = reporte.DetectionCMaterial ?? new List<string>();
                    existingReport.DetectionCMeasurement = reporte.DetectionCMeasurement ?? new List<string>();
                    existingReport.DetectionCEnvironment = reporte.DetectionCEnvironment ?? new List<string>();
                    existingReport.DetectionCRank = reporte.DetectionCRank ?? new List<int>();

                    // 4. Actualizar las listas de Root Cause Analysis
                    existingReport.FactorUno = reporte.FactorUno ?? new List<string>();
                    existingReport.FactorUnoPrimerWhy = reporte.FactorUnoPrimerWhy ?? new List<string>();
                    existingReport.FactorUnoSegundoWhy = reporte.FactorUnoSegundoWhy ?? new List<string>();
                    existingReport.FactorUnoTercerWhy = reporte.FactorUnoTercerWhy ?? new List<string>();
                    existingReport.FactorUnoCuartoWhy = reporte.FactorUnoCuartoWhy ?? new List<string>();
                    existingReport.FactorUnoQuintoWhy = reporte.FactorUnoQuintoWhy ?? new List<string>();
                    existingReport.FactorUnoCorrectiveActions = reporte.FactorUnoCorrectiveActions ?? new List<string>();

                    existingReport.FactorDos = reporte.FactorDos ?? new List<string>();
                    existingReport.FactorDosPrimerWhy = reporte.FactorDosPrimerWhy ?? new List<string>();
                    existingReport.FactorDosSegundoWhy = reporte.FactorDosSegundoWhy ?? new List<string>();
                    existingReport.FactorDosTercerWhy = reporte.FactorDosTercerWhy ?? new List<string>();
                    existingReport.FactorDosCuartoWhy = reporte.FactorDosCuartoWhy ?? new List<string>();
                    existingReport.FactorDosQuintoWhy = reporte.FactorDosQuintoWhy ?? new List<string>();
                    existingReport.FactorDosCorrectiveActions = reporte.FactorDosCorrectiveActions ?? new List<string>();

                    existingReport.FactorTres = reporte.FactorTres ?? new List<string>();
                    existingReport.FactorTresPrimerWhy = reporte.FactorTresPrimerWhy ?? new List<string>();
                    existingReport.FactorTresSegundoWhy = reporte.FactorTresSegundoWhy ?? new List<string>();
                    existingReport.FactorTresTercerWhy = reporte.FactorTresTercerWhy ?? new List<string>();
                    existingReport.FactorTresCuartoWhy = reporte.FactorTresCuartoWhy ?? new List<string>();
                    existingReport.FactorTresQuintoWhy = reporte.FactorTresQuintoWhy ?? new List<string>();
                    existingReport.FactorTresCorrectiveActions = reporte.FactorTresCorrectiveActions ?? new List<string>();

                    // 5. Actualizar D5 Permanent Corrective Actions - Occurrence
                    existingReport.OccurrenceItems = reporte.OccurrenceItems ?? new List<string>();
                    existingReport.OccurrenceAction = reporte.OccurrenceAction ?? new List<string>();
                    existingReport.OccurrenceResponsable = reporte.OccurrenceResponsable ?? new List<string>();
                    existingReport.OccurrenceDepartment = reporte.OccurrenceDepartment ?? new List<string>();
                    existingReport.OccurrenceAmef = reporte.OccurrenceAmef ?? new List<string>();
                    existingReport.OccurrenceCp = reporte.OccurrenceCp ?? new List<string>();

                    // Procesar fechas de Occurrence
                    var occurrenceOpeningDates = new List<DateTime>();
                    var occurrenceCloseDates = new List<DateTime>();

                    if (Request.Form.ContainsKey("OccurrenceOpeningDate"))
                    {
                        foreach (var dateStr in Request.Form["OccurrenceOpeningDate"])
                        {
                            if (DateTime.TryParse(dateStr, out var date))
                            {
                                occurrenceOpeningDates.Add(date);
                            }
                        }
                    }
                    existingReport.OccurrenceOpeningDate = occurrenceOpeningDates;

                    if (Request.Form.ContainsKey("OccurrenceCloseDate"))
                    {
                        foreach (var dateStr in Request.Form["OccurrenceCloseDate"])
                        {
                            if (DateTime.TryParse(dateStr, out var date))
                            {
                                occurrenceCloseDates.Add(date);
                            }
                        }
                    }
                    existingReport.OccurrenceCloseDate = occurrenceCloseDates;

                    // 6. Actualizar D5 Permanent Corrective Actions - Detection
                    existingReport.DetectionItems = reporte.DetectionItems ?? new List<string>();
                    existingReport.DetectionAction = reporte.DetectionAction ?? new List<string>();
                    existingReport.DetectionResponsable = reporte.DetectionResponsable ?? new List<string>();
                    existingReport.DetectionDepartment = reporte.DetectionDepartment ?? new List<string>();
                    existingReport.DetectionAmef = reporte.DetectionAmef ?? new List<string>();
                    existingReport.DetectionCp = reporte.DetectionCp ?? new List<string>();

                    // Procesar fechas de Detection
                    var detectionOpeningDates = new List<DateTime>();
                    var detectionCloseDates = new List<DateTime>();

                    if (Request.Form.ContainsKey("DetectionOpeningDate"))
                    {
                        foreach (var dateStr in Request.Form["DetectionOpeningDate"])
                        {
                            if (DateTime.TryParse(dateStr, out var date))
                            {
                                detectionOpeningDates.Add(date);
                            }
                        }
                    }
                    existingReport.DetectionOpeningDate = detectionOpeningDates;

                    if (Request.Form.ContainsKey("DetectionCloseDate"))
                    {
                        foreach (var dateStr in Request.Form["DetectionCloseDate"])
                        {
                            if (DateTime.TryParse(dateStr, out var date))
                            {
                                detectionCloseDates.Add(date);
                            }
                        }
                    }
                    existingReport.DetectionCloseDate = detectionCloseDates;

                    // Manejo de archivos existentes
                    if (ArchivosAMantener != null && ArchivosAMantener.Any())
                    {
                        var nuevaListaEvidencia = new List<byte[]>();
                        for (int i = 0; i < existingReport.EvidenciaFotografica?.Count; i++)
                        {
                            if (ArchivosAMantener.Contains(i))
                            {
                                nuevaListaEvidencia.Add(existingReport.EvidenciaFotografica[i]);
                            }
                        }
                        existingReport.EvidenciaFotografica = nuevaListaEvidencia;
                    }

                    // Manejar la evidencia fotográfica nueva
                    if (EvidenciaFotografica != null && EvidenciaFotografica.Count > 0)
                    {
                        existingReport.EvidenciaFotografica ??= new List<byte[]>();

                        foreach (var file in EvidenciaFotografica)
                        {
                            if (file.Length > 0)
                            {
                                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
                                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                                if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                                {
                                    ModelState.AddModelError("EvidenciaFotografica", $"Tipo de archivo no permitido: {file.FileName}");
                                    continue;
                                }

                                if (file.Length > 5 * 1024 * 1024)
                                {
                                    ModelState.AddModelError("EvidenciaFotografica", $"Archivo demasiado grande (máximo 5MB): {file.FileName}");
                                    continue;
                                }

                                using var ms = new MemoryStream();
                                await file.CopyToAsync(ms);
                                existingReport.EvidenciaFotografica.Add(ms.ToArray());
                            }
                        }
                    }

                    // Manejar PpmReport antes de guardar
                    await ManejarPpmReport(existingReport);

                    // Guardar cambios
                    await _context.SaveChangesAsync();

                    // Notificaciones
                    if (requiereNotificacion)
                    {
                        await EnviarCorreoNotificacion(existingReport);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ReporteExists(reporte.Id))
                    {
                        return NotFound();
                    }
                    _logger.LogError(ex, "Error de concurrencia al editar reporte");
                    ModelState.AddModelError("", "El registro fue modificado por otro usuario. Por favor, actualice y vuelva a intentar.");
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Error al guardar reporte");
                    ModelState.AddModelError("", "Error al guardar en la base de datos. Verifique los datos.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error inesperado al editar reporte");
                    ModelState.AddModelError("", "Ocurrió un error inesperado.");
                }
            }

            // Recargar ViewData si falla
            RecargarViewData();

            // Recargar archivos existentes para la vista
            var reporteActual = await _context.Reportes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reporteActual != null && reporteActual.EvidenciaFotografica != null)
            {
                ViewBag.ArchivosExistentes = reporteActual.EvidenciaFotografica
                    .Select((data, index) => new { Index = index, Data = data })
                    .ToList();
            }
            else
            {
                ViewBag.ArchivosExistentes = new List<object>();
            }

            return View(reporte);
        }

        // Nuevos métodos auxiliares

        private void RecargarViewData()
        {
            ViewBag.ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Select-", Selected = true },
                new SelectListItem { Value = "Bajo", Text = "Low" },
                new SelectListItem { Value = "Medio", Text = "Medium" },
                new SelectListItem { Value = "Alto", Text = "High" }
            };

            ViewBag.LineaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                new SelectListItem { Value = "ALTERNATOR", Text = "ALTERNATOR" },
                new SelectListItem { Value = "SSU Circuit Board", Text = "SSU Circuit Board" },
                new SelectListItem { Value = "FOB", Text = "FOB" },
                new SelectListItem { Value = "RCV", Text = "RCV" },
                new SelectListItem { Value = "CM", Text = "CM" },
                new SelectListItem { Value = "EPS(3G)", Text = "EPS(3G)" },
                new SelectListItem { Value = "PT BCM", Text = "PT BCM" },
                new SelectListItem { Value = "PT LFU", Text = "PT LFU" },
                new SelectListItem { Value = "EPS", Text = "EPS" },
                new SelectListItem { Value = "CID", Text = "CID" },
                new SelectListItem { Value = "LCM", Text = "LCM" },
                new SelectListItem { Value = "AMP", Text = "AMP" },
                new SelectListItem { Value = "R1", Text = "R1" },
                new SelectListItem { Value = "PT CM", Text = "PT CM" },
                new SelectListItem { Value = "PT DISPLAY", Text = "PT DISPLAY" }
            };
        }

        private bool RequiereNotificacion(Reporte existente, Reporte modificado)
        {
            // Verificar cambios en campos importantes que requieran notificación
            return modificado.Tipo != existente.Tipo ||
                   modificado.TituloProblema != existente.TituloProblema ||
                   modificado.ProblemRank != existente.ProblemRank ||
                   modificado.Responsabilidad != existente.Responsabilidad ||
                   modificado.Linea != existente.Linea ||
                   modificado.ImpactoPPM != existente.ImpactoPPM;
        }

        private async Task EnviarCorreoNotificacion(Reporte reporte)
        {
            var lineaDestinatarios = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["STARTER"] = "rsqtest@meax.mx",
                ["ALTERNATOR"] = "rsqtest@meax.mx",
                ["EPS (3G)"] = "rsqtest@meax.mx",
                ["PT EPS"] = "rsqtest@meax.mx",
                ["PT SSU"] = "rsqtest@meax.mx",
                ["PT FOB"] = "rsqtest@meax.mx",
                ["PT RCV"] = "rsqtest@meax.mx",
                ["PT BCM"] = "rsqtest@meax.mx",
                ["PT LFU"] = "rsqtest@meax.mx",
                ["EPS"] = "rsqtest@meax.mx",
                ["CM"] = "rsqtest@meax.mx",
                ["LCM"] = "rsqtest@meax.mx",
                ["AMP"] = "rsqtest@meax.mx",
                ["R1"] = "rsqtest@meax.mx",
                ["CID"] = "rsqtest@meax.mx",
                ["PT CM"] = "rsqtest@meax.mx",
                ["DEFAULT"] = "rsqtest@meax.mx"
            };

            string destinatario;
            switch (reporte.Responsabilidad)
            {
                case ResponsabilidadOpciones.Meax:
                    destinatario = lineaDestinatarios.TryGetValue(reporte.Linea ?? "", out var email)
                                ? email
                                : lineaDestinatarios["DEFAULT"];
                    break;
                case ResponsabilidadOpciones.Proveedor:
                    destinatario = "Akaren.Gonzalez@meax.mx";
                    break;
                default:
                    destinatario = "Sofia.Ramirez@meax.mx";
                    break;
            }

            string asunto = "Update Notification: Changes Made to Customer Report";
            string cuerpo = $@"Dear Team,

                    Please be advised that significant changes have been made to the {reporte.Tipo.ToString()} report 
                    from customer ""{reporte.Customer}"" regarding the issue ""{reporte.TituloProblema}"". 
                    The updated information is now contained in CAR ({reporte.Id}).

                    Kindly review the changes and proceed accordingly.

                    Best Regards.";

            await _emailService.SendEmailAsync(destinatario, asunto, cuerpo, "rsqtest@meax.mx");
        }

        private async Task ManejarPpmReport(Reporte reporte)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Habilitar IDENTITY_INSERT para SQL Server
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.qcPpmReport ON");

                var ppmReport = await _context.PpmReports.FindAsync(reporte.Id);

                if (reporte.ImpactoPPM)
                {
                    if (ppmReport == null)
                    {
                        ppmReport = new PpmReport
                        {
                            Id = reporte.Id, // Mismo ID explícito
                            Fecha = reporte.DateOfClose ?? reporte.Fecha,
                            Linea = reporte.Linea,
                        Customer = reporte.Customer,
                        CustomerClaimNumber = reporte.CustomerClaimNumber,
                        MotherFactory = reporte.MotherFactory,
                        NumParteAfectado = reporte.NumParteAfectado,
                        CustomerPartNumber = reporte.CustomerPartNumber,
                        CuantosP = reporte.CuantosP,
                        Lote = reporte.Lote,
                        Tipo = reporte.Tipo,
                        Mileage = reporte.Mileage,
                        TituloProblema = reporte.TituloProblema,
                        InvestigationReport = reporte.InvestigationReport,
                        DateOfClose = reporte.DateOfClose,
                        ImpactoPPM = true,
                        Comentarios = reporte.Comentarios,
                        Responsabilidad = reporte.Responsabilidad
                    };
                    _context.PpmReports.Add(ppmReport);
                    
                }
                else
                {
                    // Actualizar el existente (que ya tiene el mismo ID)
                    ppmReport.Fecha = reporte.DateOfClose ?? reporte.Fecha;
                    ppmReport.Linea = reporte.Linea;
                    ppmReport.Customer = reporte.Customer;
                    ppmReport.CustomerClaimNumber = reporte.CustomerClaimNumber;
                    ppmReport.MotherFactory = reporte.MotherFactory;
                    ppmReport.NumParteAfectado = reporte.NumParteAfectado;
                    ppmReport.CustomerPartNumber = reporte.CustomerPartNumber;
                    ppmReport.CuantosP = reporte.CuantosP;
                    ppmReport.Lote = reporte.Lote;
                    ppmReport.Tipo = reporte.Tipo;
                    ppmReport.Mileage = reporte.Mileage;
                    ppmReport.TituloProblema = reporte.TituloProblema;
                    ppmReport.InvestigationReport = reporte.InvestigationReport;
                    ppmReport.DateOfClose = reporte.DateOfClose;
                    ppmReport.ImpactoPPM = true;
                    ppmReport.Comentarios = reporte.Comentarios;
                    ppmReport.Responsabilidad = reporte.Responsabilidad;
                    _context.PpmReports.Update(ppmReport);
                }
            }
            else if (ppmReport != null)
            {
                // Eliminar si existe y no tiene impacto PPM
                _context.PpmReports.Remove(ppmReport);              
            }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                // Deshabilitar IDENTITY_INSERT
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.qcPpmReport OFF");
            }
        }

        /// <summary>
        /// Muestra el formulario para confirmar la eliminación de un reporte.
        /// </summary>
        /// <param name="id">El ID del reporte a eliminar.</param>
        /// <returns>Una vista con el formulario de confirmación.</returns>
        // GET: Reportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        /// <summary>
        /// Procesa la eliminación de un reporte.
        /// </summary>
        /// <param name="id">El ID del reporte a eliminar.</param>
        /// <returns>Redirige a la lista de reportes.</returns>
        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Verifica si un reporte existe en la base de datos.
        /// </summary>
        /// <param name="id">El ID del reporte.</param>
        /// <returns>True si el reporte existe; de lo contrario, False.</returns>
        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.Id == id);
        }

        /// <summary>
        /// Exporta los reportes a un archivo Excel.
        /// </summary>
        /// <returns>Un archivo Excel con los datos de los reportes.</returns>
        [HttpGet]
        public IActionResult ExportToExcel(int id, string linea)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Obtener el código de negocio basado en la línea
            string codigoNegocio = BusinessUnitMapping.GetBusinessUnitCode(linea);
            int anioActual = DateTime.Now.Year % 100; // Obtiene los últimos 2 dígitos del año

            // Obtener el consecutivo
            int consecutivo = ObtenerSiguienteConsecutivo(codigoNegocio, DateTime.Now.Year);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/template.xlsx");

            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                // Selecciona la hoja llamada "CAR Español"
                var worksheet = package.Workbook.Worksheets["CAR Español"];
                // Verifica si la hoja existe
                if (worksheet == null)
                {
                    Debug.WriteLine("La hoja 'CAR Español' no existe en el archivo de plantilla.");
                    return NotFound("La hoja 'CAR Español' no existe en el archivo de plantilla.");
                }

                // Obtener el registro específico por ID
                var reporte = _context.Reportes.FirstOrDefault(r => r.Id == id);

                if (reporte != null)
                {
                    // Debug: Imprime los valores de las propiedades del reporte
                    Debug.WriteLine($"Fecha: {reporte.Fecha}");
                    Debug.WriteLine($"ProblemRank: {reporte.ProblemRank}");
                    Debug.WriteLine($"UserName: {reporte.UserName}");
                    Debug.WriteLine($"Linea: {reporte.Linea}");
                    Debug.WriteLine($"Tipo: {reporte.Tipo}");
                    Debug.WriteLine($"QueP: {reporte.QueP}");
                    Debug.WriteLine($"QuienP: {reporte.QuienP}");
                    Debug.WriteLine($"DondeP: {reporte.DondeP}");
                    Debug.WriteLine($"CuandoP: {reporte.CuandoP}");
                    Debug.WriteLine($"PorqueP: {reporte.PorqueP}");
                    Debug.WriteLine($"ComoP: {reporte.ComoP}");
                    Debug.WriteLine($"CuantosP: {reporte.CuantosP}");

                    worksheet.Cells["W6"].Value = reporte.Fecha;
                    worksheet.Cells["F6"].Value = reporte.ProblemRank;
                    worksheet.Cells["C11"].Value = reporte.NumParteAfectado;
                    worksheet.Cells["H11"].Value = reporte.CustomerPartNumber;
                    worksheet.Cells["L11"].Value = reporte.Descripcion;
                    worksheet.Cells["P11"].Value = reporte.Lote;
                    worksheet.Cells["T11"].Value = reporte.UserName;
                    worksheet.Cells["C13"].Value = reporte.Fecha;
                    worksheet.Cells["F13"].Value = reporte.Linea;
                    worksheet.Cells["I26"].Value = reporte.QueP;
                    worksheet.Cells["I27"].Value = reporte.QuienP;
                    worksheet.Cells["J28"].Value = reporte.DondeP;
                    worksheet.Cells["J29"].Value = reporte.CuandoP;
                    worksheet.Cells["H30"].Value = reporte.PorqueP;
                    worksheet.Cells["I31"].Value = reporte.ComoP;
                    worksheet.Cells["M32"].Value = reporte.CuantosP;

                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    // Generar nombre del archivo con el formato: CAR-CODIGO-CONSECUTIVO-AÑO
                    string nombreArchivo = reporte.NombreCar ?? $"CAR-{codigoNegocio}-{consecutivo:D2}-{anioActual:D2}.xlsx";


                    return File(stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
                else
                {
                    // Debug: Si no hay datos, imprime un mensaje
                    Debug.WriteLine($"No se encontró el reporte con ID {id}");
                    return NotFound($"No se encontró el reporte con ID {id}");
                }
            }
        }

        private int ObtenerSiguienteConsecutivo(string codigoNegocio, int anio)
        {
            var registro = _context.ConsecutivosArchivos
                .FirstOrDefault(c => c.CodigoNegocio == codigoNegocio && c.Anio == anio);

            if (registro == null)
            {
                registro = new ConsecutivoArchivo
                {
                    CodigoNegocio = codigoNegocio,
                    UnidadNegocio = codigoNegocio, // O usa BusinessUnitMapping.GetNombreCompleto si lo implementaste
                    Anio = anio,
                    Consecutivo = 1
                };
                _context.ConsecutivosArchivos.Add(registro);
            }
            else
            {
                registro.Consecutivo++;
            }

            _context.SaveChanges();
            return registro.Consecutivo;
        }

        [HttpGet]
        public async Task<IActionResult> GetNextClaimNumber(string customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer))
                    return BadRequest(new { error = "Se requiere un cliente" });

                int nextNumber = await _context.Reportes
                    .Where(r => r.Customer == customer)
                    .MaxAsync(r => (int?)r.CustomerClaimNumber) ?? 0;
                nextNumber++;

                return Json(new { nextNumber });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar número de reclamo");
                return StatusCode(500, new { error = "Error interno al generar número" });
            }
        }


    }
}
