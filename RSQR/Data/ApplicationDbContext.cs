using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSQR.Models;

namespace RSQR.Data
{
    /// <summary>
    /// Representa el contexto de la base de datos para la aplicación, incluyendo
    /// las tablas de Identity y los modelos personalizados.
    /// </summary>
    /// <remarks>
    /// Hereda de IdentityDbContext para integrar el sistema de autenticación ASP.NET Core Identity.
    /// </remarks>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Inicializa una nueva instancia del contexto de la base de datos.
        /// </summary>
        /// <param name="options">Las opciones de configuración para este contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad Reporte.
        /// </summary>
        public DbSet<Reporte> Reportes { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad ConsecutivoArchivo.
        /// </summary>
        public DbSet<ConsecutivoArchivo> ConsecutivosArchivos { get; set; }

        /// <summary>
        /// Obtiene o establece el DbSet para la entidad PpmReport.
        /// </summary>
        public DbSet<PpmReport> PpmReports { get; set; }

        /// <summary>
        /// Configura el modelo de la base de datos, incluyendo las relaciones y restricciones.
        /// </summary>
        /// <param name="modelBuilder">El constructor usado para crear el modelo.</param>
        /// <remarks>
        /// Este método se llama cuando el modelo para un contexto derivado se inicializa.
        /// Puede usarse para configurar el modelo descubierto por convención antes de que
        /// se bloquee y se use para inicializar el contexto.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Fecha).IsRequired();
                entity.Property(e => e.UserName).HasMaxLength(256);
            });
        }
    }
}