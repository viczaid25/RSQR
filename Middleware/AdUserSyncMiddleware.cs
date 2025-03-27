using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AdUserSyncMiddleware
{
    private readonly RequestDelegate _next;

    public AdUserSyncMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AdUserManagerService adUserManagerService)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            await adUserManagerService.SyncAdUserAsync(context.User);
        }

        await _next(context);
    }
}