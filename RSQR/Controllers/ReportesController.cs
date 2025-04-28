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
        public ReportesController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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
        public async Task<IActionResult> Create([Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado,Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP,Lote,CustomerClaimNumber,ContencionItems,ContencionActividades,ContencionResponsables,ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios,Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,EvidenciaFotografica,Customer,MotherFactory,CustomerPartNumber,Mileage,InvestigationReport,DateOfClose,ImpactoPPM,Responsabilidad")] Reporte reporte, List<IFormFile>? EvidenciaFotografica)
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

                _context.Add(reporte);
                await _context.SaveChangesAsync();

                // Mapeo de líneas a destinatarios
                var lineaDestinatarios = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["STARTER"] = "meax_qc_st@meax.mx",
                    ["ALTERNATOR"] = "meax_qc_alt@meax.mx",
                    ["EPS (3G)"] = "meax_qc_eps@meax.mx",
                    ["PT EPS"] = "meax_qc_eps@meax.mx",
                    ["PT SSU"] = "meax_qc_eps@meax.mx",
                    ["PT FOB"] = "meax_qc_eps@meax.mx",
                    ["PT RCV"] = "meax_qc_eps@meax.mx",
                    ["PT BCM"] = "meax_qc_eps@meax.mx",
                    ["PT LFU"] = "meax_qc_eps@meax.mx",
                    ["EPS"] = "meax_qc_eps@meax.mx",
                    ["CM"] = "meax_qc_cm@meax.mx",
                    ["LCM"] = "meax_qc_mm@meax.mx",
                    ["AMP"] = "meax_qc_mm@meax.mx",
                    ["R1"] = "meax_qc_mm@meax.mx",
                    ["CID"] = "meax_qc_mm@meax.mx",
                    ["PT CM"] = "meax_qc_cm@meax.mx",
                    ["DEFAULT"] = "Zaid.Garcia@meax.mx" // Valor por defecto
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

                await _emailService.SendEmailAsync(destinatario, asunto, cuerpo, "Zaid.Garcia@meax.mx");

                // Manejo de PPM si es necesario
                if (reporte.ImpactoPPM)
                {
                    var ppmReport = new PpmReport
                    {
                        // ... (tu código existente para PPM) ...
                    };
                    _context.Add(ppmReport);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
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
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,ProblemRank,UserName,Linea,Tipo,TituloProblema,CincoM,NumParteAfectado,Descripcion,DescripcionProblema,QueP,PorqueP,DondeP,QuienP,CuandoP,CuantosP,ComoP,Lote,CustomerClaimNumber,PreguntasAdicionales,ContencionItems,ContencionActividades,ContencionResponsables,ContencionFechasInicio,AlertaCalidad,Disposicion,EntrevistaInvolucrados,Comentarios,Severidad,Ocurrencia,Deteccion,AP_NPR,ModoFalla,ControlesEstablecidos,EvidenciaFotografica")] Reporte reporte)
        {
            if (id != reporte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExists(reporte.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reporte);
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
        public IActionResult ExportToExcel(string linea)
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

                var reportes = _context.Reportes.ToList();

                if (reportes.Count > 0)
                {
                    var reporte = reportes[0];

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


                    worksheet.Cells["W6"].Value = reporte.Fecha; // Primer dato en S11
                    worksheet.Cells["F6"].Value = reporte.ProblemRank; // Segundo dato en E6
                    worksheet.Cells["C11"].Value = reporte.NumParteAfectado;
                    worksheet.Cells["H11"].Value = reporte.CustomerPartNumber;
                    worksheet.Cells["L11"].Value = reporte.Descripcion;
                    worksheet.Cells["P11"].Value = reporte.Lote;
                    worksheet.Cells["T11"].Value = reporte.UserName; // Otro dato en C5
                    worksheet.Cells["C13"].Value = reporte.Fecha;
                    worksheet.Cells["F13"].Value = reporte.Linea; // Otro dato en F7
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
                    string nombreArchivo = $"CAR-{codigoNegocio}-{consecutivo:D2}-{anioActual:D2}.xlsx";

                    return File(stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
                else
                {
                    // Debug: Si no hay datos, imprime un mensaje
                    Debug.WriteLine("No se encontraron datos en la base de datos.");
                    return NotFound("No hay datos para exportar.");
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
