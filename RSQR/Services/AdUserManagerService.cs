using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

public class AdUserManagerService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdUserManagerService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SyncAdUserAsync(ClaimsPrincipal principal)
    {
        var userName = principal.Identity.Name; // Obtiene el nombre de usuario de AD

        // Busca el usuario en la base de datos de Identity
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            // Si el usuario no existe, créalo
            user = new IdentityUser
            {
                UserName = userName,
                Email = $"{userName}@dominio.com" // Puedes ajustar el correo electrónico según tu necesidad
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                // Maneja el error si no se puede crear el usuario
                throw new System.Exception("No se pudo crear el usuario en Identity.");
            }
        }
    }
}