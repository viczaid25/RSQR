using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RSQR.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RSQR.Controllers
{
    /// <summary>
    /// Controlador para gestionar la autenticación de usuarios.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly ActiveDirectoryService _adService;

        public AccountController(ActiveDirectoryService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Procesa el formulario de inicio de sesión.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // 1) Validación básica de campos vacíos
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Usuario y contraseña son obligatorios.";
                return View();
            }

            // 2) Validar contra Active Directory
            string? displayName;
            try
            {
                displayName = _adService.GetDisplayName(username, password);
            }
            catch
            {
                // Si hay un error en la conexión a AD, no queremos que pase el login
                ViewBag.ErrorMessage = "Hubo un problema al validar contra Active Directory.";
                return View();
            }

            if (string.IsNullOrEmpty(displayName))
            {
                // AD rechazó usuario/contraseña
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos (AD).";
                return View();
            }

            // 3) AD OK → crear claims y cookie de autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("DisplayName", displayName),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "MeaxAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            await HttpContext.SignInAsync(
                "MeaxAuth",                         // 🔹 mismo esquema que en Program.cs
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MeaxAuth");
            return RedirectToAction("Login");
        }
    }
}
