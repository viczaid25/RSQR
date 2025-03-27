using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        /// <summary>
        /// Constructor del controlador AccountController.
        /// </summary>
        /// <param name="adService">Servicio de Active Directory para autenticación.</param>
        public AccountController(ActiveDirectoryService adService)
        {
            _adService = adService;
        }

        /// <summary>
        /// Muestra la vista de inicio de sesión.
        /// </summary>
        /// <returns>La vista de inicio de sesión.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Procesa el formulario de inicio de sesión.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>
        /// Redirige al usuario a la página de inicio si la autenticación es exitosa.
        /// Muestra un mensaje de error en la vista de inicio de sesión si la autenticación falla.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string displayName = _adService.GetDisplayName(username, password);

            if (!string.IsNullOrEmpty(displayName))
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("DisplayName", displayName),
            new Claim(ClaimTypes.Role, "User")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        /// <returns>Redirige al usuario a la vista de inicio de sesión.</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Cerrar la sesión y eliminar la cookie de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}