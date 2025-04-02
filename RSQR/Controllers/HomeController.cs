using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSQR.Models;
using System.Diagnostics;

namespace RSQR.Controllers
{
    /// <summary>
    /// Controlador principal de la aplicación. Gestiona las vistas de inicio, privacidad y errores.
    /// </summary>
    /// <remarks>
    /// Este controlador está protegido con autorización, por lo que solo los usuarios autenticados pueden acceder a sus acciones.
    /// </remarks>
    [Authorize] // Solo usuarios autenticados pueden acceder a este controlador
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        /// <summary>
        /// Constructor del controlador HomeController.
        /// </summary>
        /// <param name="logger">Instancia de ILogger para registrar eventos y errores.</param>
        public HomeController(ILogger<HomeController> logger)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _logger = logger;
        }

        /// <summary>
        /// Muestra la vista de inicio (Index).
        /// </summary>
        /// <returns>La vista de inicio.</returns>
        public IActionResult Index()
        {
            return View();
        }

#pragma warning disable CS1591
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Muestra la vista de error.
        /// </summary>
        /// <returns>La vista de error con detalles del error ocurrido.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}