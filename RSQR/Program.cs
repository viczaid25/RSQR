using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using RSQR.Data;
using RSQR.Models;
using RSQR.Services;
using System.Diagnostics;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext RSQR (si lo usas para algo; si no, también puedes quitarlo)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2) DataProtection (lo dejamos listo para el futuro SSO)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\DataProtectionKeys\MeaxAuth"))
    .SetApplicationName("MeaxAuth");

// 3) Servicios propios
builder.Services.AddSingleton(new ActiveDirectoryService("ad.meax.mx"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

// 4) Autenticación: SOLO cookie "MeaxAuth" para RSQR
builder.Services
    .AddAuthentication("MeaxAuth")
    .AddCookie("MeaxAuth", options =>
    {
        options.Cookie.Name = ".MEAX.AUTH";
        options.Cookie.Path = "/";
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.Lax;

        // 🔹 cuando falte autenticación, manda al Hub
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                // Usa el mismo esquema + host + puerto de la petición actual
                var request = context.Request;

                // ejemplo: http://10.228.25.13:81
                var baseUrl = $"{request.Scheme}://{request.Host.Value}";

                // login del Hub, que está en la raíz del mismo sitio
                var hubLoginUrl = baseUrl + "/Account/Login";

                var returnUrl = request.PathBase + request.Path + request.QueryString;
                var redirectUrl = hubLoginUrl + "?returnUrl=" + Uri.EscapeDataString(returnUrl);

                context.Response.Redirect(redirectUrl);
                return Task.CompletedTask;
            }
        };

        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });


builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Para pruebas, si usas sólo HTTP se puede comentar esta línea
// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    Debug.WriteLine($"[RSQR] Autenticado: {context.User.Identity?.IsAuthenticated}");
    Debug.WriteLine($"[RSQR] Usuario: {context.User.Identity?.Name}");
    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
