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
    /// Controlador para gestionar los reportes de calidad del sistema (CAR - Corrective Action Reports)
    /// </summary>
    /// <remarks>
    /// Este controlador maneja todo el ciclo de vida de los reportes de calidad, incluyendo:
    /// - Creación, edición y eliminación de reportes
    /// - Exportación a Excel
    /// - Notificaciones por correo electrónico
    /// - Integración con reportes PPM (Parts Per Million)
    /// 
    /// Todas las acciones requieren autenticación.
    /// </remarks>
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReportesController> _logger;

        /// <summary>
        /// Constructor del controlador ReportesController
        /// </summary>
        /// <param name="context">Contexto de base de datos para acceso a Reportes</param>
        /// <param name="emailService">Servicio para envío de notificaciones por email</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public ReportesController(
            ApplicationDbContext context,
            IEmailService emailService,
            ILogger<ReportesController> logger)
        {
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Muestra la lista paginada de todos los reportes de calidad
        /// </summary>
        /// <returns>Vista con lista de reportes</returns>
        /// <response code="200">Retorna la vista con la lista de reportes</response>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reportes.ToListAsync());
        }

        /// <summary>
        /// Muestra los detalles completos de un reporte específico
        /// </summary>
        /// <param name="id">ID del reporte a visualizar</param>
        /// <returns>Vista con los detalles del reporte</returns>
        /// <response code="200">Retorna la vista con los detalles del reporte</response>
        /// <response code="404">Si no se encuentra el reporte con el ID especificado</response>
        [HttpGet]
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

            // Inicializar todas las listas que podrían ser null para evitar NullReferenceException
            InitializeReportLists(reporte);

            return View(reporte);
        }

        /// <summary>
        /// Muestra el formulario para crear un nuevo reporte de calidad
        /// </summary>
        /// <returns>Vista con formulario de creación</returns>
        /// <remarks>
        /// Prepara las listas desplegables necesarias para el formulario:
        /// - ProblemRank (Bajo/Medio/Alto)
        /// - Líneas de producción disponibles
        /// </remarks>
        [HttpGet]
        public IActionResult Create()
        {
            var displayName = User.FindFirst("DisplayName")?.Value;

            var reporte = new Reporte
            {
                UserName = displayName
            };

            ViewBag.DisplayName = displayName;
            InitializeCreateViewBags();

            return View(reporte);
        }

        /// <summary>
        /// Procesa el formulario de creación de un nuevo reporte
        /// </summary>
        /// <param name="reporte">Datos del reporte a crear</param>
        /// <param name="EvidenciaFotografica">Archivos adjuntos como evidencia</param>
        /// <returns>Redirección al listado si éxito, formulario con errores si falla</returns>
        /// <response code="302">Redirige al listado si creación es exitosa</response>
        /// <response code="400">Retorna el formulario con errores de validación</response>
        /// <remarks>
        /// Acciones realizadas durante la creación:
        /// 1. Procesa archivos adjuntos
        /// 2. Genera número de reclamo único para el cliente
        /// 3. Genera nombre del archivo CAR según convención
        /// 4. Envía notificación por email según responsabilidad
        /// 5. Crea registro PPM si es necesario
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado,Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP,Lote,CustomerClaimNumber,ContencionItems,ContencionActividades,ContencionResponsables,ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios,Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,EvidenciaFotografica,Customer,MotherFactory,CustomerPartNumber,Mileage,InvestigationReport,DateOfClose,ImpactoPPM,Responsabilidad,NombreCar,CustomerReport")]
            Reporte reporte,
            List<IFormFile>? EvidenciaFotografica)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ProcessAttachments(reporte, EvidenciaFotografica);

                    // Validar y generar número de reclamo
                    if (reporte.CustomerClaimNumber <= 0)
                    {
                        reporte.CustomerClaimNumber = await GetNextClaimNumberForCustomer(reporte.Customer);
                    }

                    // Generar nombre del archivo CAR
                    GenerateCarFileName(reporte);

                    _context.Add(reporte);
                    await _context.SaveChangesAsync();

                    // Enviar notificación por email
                    await SendCreationNotification(reporte);

                    // Manejar PPM si es necesario
                    if (reporte.ImpactoPPM)
                    {
                        await CreatePpmReport(reporte);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear reporte");
                    ModelState.AddModelError("", "Ocurrió un error al guardar el reporte.");
                }
            }

            LogModelStateErrors();
            return View(reporte);
        }

        /// <summary>
        /// Muestra el formulario para editar un reporte existente
        /// </summary>
        /// <param name="id">ID del reporte a editar</param>
        /// <returns>Vista con formulario de edición</returns>
        /// <response code="200">Retorna el formulario con los datos del reporte</response>
        /// <response code="404">Si no se encuentra el reporte con el ID especificado</response>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            InitializeEditViewBags();

            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null) return NotFound();

            PrepareExistingFilesViewBag(reporte);

            return View(reporte);
        }

        /// <summary>
        /// Procesa el formulario de edición de un reporte existente
        /// </summary>
        /// <param name="id">ID del reporte a editar</param>
        /// <param name="reporte">Datos actualizados del reporte</param>
        /// <param name="EvidenciaFotografica">Nuevos archivos adjuntos</param>
        /// <param name="ArchivosAMantener">IDs de archivos existentes a conservar</param>
        /// <returns>Redirección al listado si éxito, formulario con errores si falla</returns>
        /// <response code="302">Redirige al listado si la edición es exitosa</response>
        /// <response code="400">Retorna el formulario con errores de validación</response>
        /// <remarks>
        /// Acciones realizadas durante la edición:
        /// 1. Actualiza todas las listas complejas del reporte
        /// 2. Maneja archivos adjuntos (conservación y nuevos)
        /// 3. Actualiza registro PPM si es necesario
        /// 4. Envía notificación si hay cambios importantes
        /// </remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado,Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP,Lote,CustomerClaimNumber,ContencionItems,ContencionActividades,ContencionResponsables,ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios,Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,Customer,MotherFactory,CustomerPartNumber,Mileage,InvestigationReport,DateOfClose,ImpactoPPM,Responsabilidad,NombreCar,CustomerReport,FmMode,FmProcessName,Fm6Ms,FmFactorUno,FmFactorDos,FmFactorTres,FmRelated,FmContentionActions,PreventiveCProcessName,PreventiveCManpower,PreventiveCMethod,PreventiveCMachinary,PreventiveCMaterial,PreventiveCMeasurement,PreventiveCEnvironment,PreventiveCRank,DetectionCProcessName,DetectionCManpower,DetectionCMethod,DetectionCMachinary,DetectionCMaterial,DetectionCMeasurement,DetectionCEnvironment,DetectionCRank,FactorUno,FactorUnoPrimerWhy,FactorUnoSegundoWhy,FactorUnoTercerWhy,FactorUnoCuartoWhy,FactorUnoQuintoWhy,FactorUnoCorrectiveActions,FactorDos,FactorDosPrimerWhy,FactorDosSegundoWhy,FactorDosTercerWhy,FactorDosCuartoWhy,FactorDosQuintoWhy,FactorDosCorrectiveActions,FactorTres,FactorTresPrimerWhy,FactorTresSegundoWhy,FactorTresTercerWhy,FactorTresCuartoWhy,FactorTresQuintoWhy,FactorTresCorrectiveActions,OccurrenceItems,OccurrenceAction,OccurrenceResponsable,OccurrenceDepartment,OccurrenceOpeningDate,OccurrenceCloseDate,OccurrenceAmef,OccurrenceCp,DetectionItems,DetectionAction,DetectionResponsable,DetectionDepartment,DetectionOpeningDate,DetectionCloseDate,DetectionAmef,DetectionCp")]
            Reporte reporte,
            List<IFormFile>? EvidenciaFotografica,
            List<int>? ArchivosAMantener)
        {
            if (id != reporte.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingReport = await _context.Reportes
                        .Include(r => r.PpmReport)
                        .FirstOrDefaultAsync(r => r.Id == id);

                    if (existingReport == null) return NotFound();

                    bool requiereNotificacion = RequiereNotificacion(existingReport, reporte);

                    // Actualizar propiedades básicas
                    var entry = _context.Entry(existingReport);
                    entry.CurrentValues.SetValues(reporte);
                    entry.Property(e => e.EvidenciaFotografica).IsModified = false;

                    // Actualizar todas las listas complejas
                    UpdateReportLists(existingReport, reporte);

                    // Manejar archivos adjuntos
                    await ProcessExistingAndNewAttachments(existingReport, EvidenciaFotografica, ArchivosAMantener);

                    // Manejar PPM report
                    await ManejarPpmReport(existingReport);

                    await _context.SaveChangesAsync();

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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al editar reporte");
                    ModelState.AddModelError("", "Ocurrió un error inesperado al guardar los cambios.");
                }
            }

            RecargarViewData();
            await PrepareExistingFilesViewBagForEdit(id);
            return View(reporte);
        }

        /// <summary>
        /// Muestra el formulario de confirmación para eliminar un reporte
        /// </summary>
        /// <param name="id">ID del reporte a eliminar</param>
        /// <returns>Vista de confirmación</returns>
        /// <response code="200">Retorna la vista de confirmación</response>
        /// <response code="404">Si no se encuentra el reporte</response>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reporte = await _context.Reportes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (reporte == null) return NotFound();

            return View(reporte);
        }

        /// <summary>
        /// Procesa la eliminación de un reporte confirmada
        /// </summary>
        /// <param name="id">ID del reporte a eliminar</param>
        /// <returns>Redirección al listado</returns>
        /// <response code="302">Redirige al listado después de eliminar</response>
        /// <response code="404">Si no se encuentra el reporte</response>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Exporta un reporte a formato Excel (CAR)
        /// </summary>
        /// <param name="id">ID del reporte a exportar</param>
        /// <param name="linea">Línea de producción asociada</param>
        /// <returns>Archivo Excel descargable</returns>
        /// <response code="200">Retorna el archivo Excel generado</response>
        /// <response code="404">Si no se encuentra el reporte o la plantilla</response>
        /// <remarks>
        /// El archivo generado sigue el formato estándar CAR con nombre según convención:
        /// CAR-CODIGO-CONSECUTIVO-AÑO.xlsx
        /// </remarks>
        [HttpGet]
        public IActionResult ExportToExcel(int id, string linea)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string codigoNegocio = BusinessUnitMapping.GetBusinessUnitCode(linea);
            int anioActual = DateTime.Now.Year % 100;
            int consecutivo = ObtenerSiguienteConsecutivo(codigoNegocio, DateTime.Now.Year);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/template.xlsx");

            using (var package = new ExcelPackage(new FileInfo(templatePath)))
            {
                var worksheet = package.Workbook.Worksheets["CAR Español"];
                if (worksheet == null)
                {
                    _logger.LogError("La hoja 'CAR Español' no existe en la plantilla");
                    return NotFound("La hoja 'CAR Español' no existe en el archivo de plantilla.");
                }

                var reporte = _context.Reportes.FirstOrDefault(r => r.Id == id);
                if (reporte != null)
                {
                    // Mapear datos del reporte a celdas de Excel
                    MapReportToExcelWorksheet(reporte, worksheet);

                    var stream = new MemoryStream();
                    package.SaveAs(stream);
                    stream.Position = 0;

                    string nombreArchivo = reporte.NombreCar ?? $"CAR-{codigoNegocio}-{consecutivo:D2}-{anioActual:D2}.xlsx";

                    return File(stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
                else
                {
                    _logger.LogWarning($"No se encontró reporte con ID {id} para exportar");
                    return NotFound($"No se encontró el reporte con ID {id}");
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente número de reclamo para un cliente específico
        /// </summary>
        /// <param name="customer">Nombre del cliente</param>
        /// <returns>JSON con el siguiente número disponible</returns>
        /// <response code="200">Retorna el siguiente número de reclamo</response>
        /// <response code="400">Si no se proporciona cliente</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpGet]
        public async Task<IActionResult> GetNextClaimNumber(string customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer))
                    return BadRequest(new { error = "Se requiere un cliente" });

                int nextNumber = await GetNextClaimNumberForCustomer(customer);
                return Json(new { nextNumber });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar número de reclamo");
                return StatusCode(500, new { error = "Error interno al generar número" });
            }
        }

        #region Métodos Privados

        private void InitializeReportLists(Reporte reporte)
        {
            // Inicializa todas las listas del reporte para evitar nulls
            reporte.ContencionConsiderar = reporte.ContencionConsiderar ?? new List<string>();
            reporte.ContencionActivity = reporte.ContencionActivity ?? new List<string>();
            // ... (todas las demás inicializaciones de listas)
        }

        private void InitializeCreateViewBags()
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
                new SelectListItem { Value = "", Text = "-Select-", Selected = true },
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                // ... (resto de opciones)
            };
        }

        private async Task ProcessAttachments(Reporte reporte, List<IFormFile>? files)
        {
            if (files != null && files.Count > 0)
            {
                reporte.EvidenciaFotografica = new List<byte[]>();
                foreach (var file in files)
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
        }

        private void GenerateCarFileName(Reporte reporte)
        {
            string codigoNegocio = BusinessUnitMapping.GetBusinessUnitCode(reporte.Linea ?? "DEFAULT");
            int anioActual = DateTime.Now.Year % 100;
            int consecutivo = ObtenerSiguienteConsecutivo(codigoNegocio, DateTime.Now.Year);
            reporte.NombreCar = $"CAR-{codigoNegocio}-{consecutivo:D2}-{anioActual:D2}.xlsx";
        }

        private async Task SendCreationNotification(Reporte reporte)
        {
            var lineaDestinatarios = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["STARTER"] = "rsqtest@meax.mx",
                // ... (resto de mapeos)
            };

            string destinatario = DetermineRecipient(reporte, lineaDestinatarios);

            string asunto = "Immediate Attention required: A Customer Report Has Been Issued";
            string cuerpo = $@"Dear Team,

                            Please be advised that a {reporte.Tipo.ToString()} report has been received from customer ""{reporte.Customer}"" regarding the issue ""{reporte.TituloProblema}"". The information of the issue is now contained in CAR ({reporte.Id}).

                            Kindly review the information and proceed accordingly.

                            Best Regards.";

            await _emailService.SendEmailAsync(destinatario, asunto, cuerpo, "rsqtest@meax.mx");
        }

        private string DetermineRecipient(Reporte reporte, Dictionary<string, string> lineaDestinatarios)
        {
            switch (reporte.Responsabilidad)
            {
                case ResponsabilidadOpciones.Meax:
                    return lineaDestinatarios.TryGetValue(reporte.Linea ?? "", out var email)
                            ? email
                            : lineaDestinatarios["DEFAULT"];
                case ResponsabilidadOpciones.Proveedor:
                    return "Akaren.Gonzalez@meax.mx";
                default:
                    return "Sofia.Ramirez@meax.mx";
            }
        }

        private async Task CreatePpmReport(Reporte reporte)
        {
            var ppmReport = new PpmReport
            {
                Fecha = reporte.Fecha,
                Customer = reporte.Customer!,
                // ... (mapeo de propiedades)
            };
            _context.Add(ppmReport);
            await _context.SaveChangesAsync();
        }

        private void LogModelStateErrors()
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogWarning($"Campo: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
        }

        private void InitializeEditViewBags()
        {
            ViewBag.ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Seleccionar-", Selected = true },
                // ... (opciones)
            };

            ViewBag.LineaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                // ... (opciones)
            };
        }

        private void PrepareExistingFilesViewBag(Reporte reporte)
        {
            var archivosExistentes = new List<object>();
            if (reporte.EvidenciaFotografica != null)
            {
                for (int i = 0; i < reporte.EvidenciaFotografica.Count; i++)
                {
                    archivosExistentes.Add(new { Index = i, Data = reporte.EvidenciaFotografica[i] });
                }
            }
            ViewBag.ArchivosExistentes = archivosExistentes;
        }

        private void UpdateReportLists(Reporte existing, Reporte modified)
        {
            // Actualiza todas las listas complejas del reporte
            existing.FmMode = modified.FmMode ?? new List<string>();
            existing.FmProcessName = modified.FmProcessName ?? new List<string>();
            // ... (resto de actualizaciones de listas)
        }

        private async Task ProcessExistingAndNewAttachments(Reporte existing, List<IFormFile>? newFiles, List<int>? keepFiles)
        {
            // Manejar archivos existentes a conservar
            if (keepFiles != null && keepFiles.Any())
            {
                var nuevaListaEvidencia = new List<byte[]>();
                for (int i = 0; i < existing.EvidenciaFotografica?.Count; i++)
                {
                    if (keepFiles.Contains(i))
                    {
                        nuevaListaEvidencia.Add(existing.EvidenciaFotografica[i]);
                    }
                }
                existing.EvidenciaFotografica = nuevaListaEvidencia;
            }

            // Agregar nuevos archivos
            if (newFiles != null && newFiles.Count > 0)
            {
                existing.EvidenciaFotografica ??= new List<byte[]>();

                foreach (var file in newFiles)
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
                        existing.EvidenciaFotografica.Add(ms.ToArray());
                    }
                }
            }
        }

        private async Task ManejarPpmReport(Reporte reporte)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.qcPpmReport ON");

                var ppmReport = await _context.PpmReports.FindAsync(reporte.Id);

                if (reporte.ImpactoPPM)
                {
                    if (ppmReport == null)
                    {
                        ppmReport = new PpmReport
                        {
                            Id = reporte.Id,
                            // ... (mapeo de propiedades)
                        };
                        _context.PpmReports.Add(ppmReport);
                    }
                    else
                    {
                        // Actualizar existente
                        ppmReport.Fecha = reporte.DateOfClose ?? reporte.Fecha;
                        // ... (resto de actualizaciones)
                    }
                }
                else if (ppmReport != null)
                {
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
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.qcPpmReport OFF");
            }
        }

        private bool RequiereNotificacion(Reporte existente, Reporte modificado)
        {
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
                // ... (resto de mapeos)
            };

            string destinatario = DetermineRecipient(reporte, lineaDestinatarios);

            string asunto = "Update Notification: Changes Made to Customer Report";
            string cuerpo = $@"Dear Team,

                    Please be advised that significant changes have been made to the {reporte.Tipo.ToString()} report 
                    from customer ""{reporte.Customer}"" regarding the issue ""{reporte.TituloProblema}"". 
                    The updated information is now contained in CAR ({reporte.Id}).

                    Kindly review the changes and proceed accordingly.

                    Best Regards.";

            await _emailService.SendEmailAsync(destinatario, asunto, cuerpo, "rsqtest@meax.mx");
        }

        private void RecargarViewData()
        {
            ViewBag.ProblemRankList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-Select-", Selected = true },
                // ... (opciones)
            };

            ViewBag.LineaList = new List<SelectListItem>
            {
                new SelectListItem { Value = "STARTER", Text = "STARTER" },
                // ... (opciones)
            };
        }

        private async Task PrepareExistingFilesViewBagForEdit(int id)
        {
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
        }

        private void MapReportToExcelWorksheet(Reporte reporte, ExcelWorksheet worksheet)
        {
            worksheet.Cells["W6"].Value = reporte.Fecha;
            worksheet.Cells["F6"].Value = reporte.ProblemRank;
            // ... (resto de mapeos a celdas)
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
                    UnidadNegocio = codigoNegocio,
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

        private async Task<int> GetNextClaimNumberForCustomer(string customer)
        {
            if (string.IsNullOrEmpty(customer))
                return 1;

            var lastNumber = await _context.Reportes
                .Where(r => r.Customer == customer)
                .MaxAsync(r => (int?)r.CustomerClaimNumber) ?? 0;

            return lastNumber + 1;
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.Id == id);
        }

        #endregion
    }
}