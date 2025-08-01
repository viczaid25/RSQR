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
            modelBuilder.Entity<Reporte>().ToTable("qcReport");
            modelBuilder.Entity<PpmReport>().ToTable("qcPpmReport");

            base.OnModelCreating(modelBuilder); // Esto es CRUCIAL para Identity

            // Configuración para PpmReport
            modelBuilder.Entity<PpmReport>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedNever();

                entity.HasOne<Reporte>()
                      .WithOne(r => r.PpmReport)
                      .HasForeignKey<PpmReport>(p => p.Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}