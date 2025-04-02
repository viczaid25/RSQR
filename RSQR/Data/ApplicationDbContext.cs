using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSQR.Models;

namespace RSQR.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<ConsecutivoArchivo> ConsecutivosArchivos { get; set; }

        public DbSet<PpmReport> PpmReports { get; set; }

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
