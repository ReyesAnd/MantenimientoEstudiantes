using Microsoft.EntityFrameworkCore;
using MantenimientoEstudiantes.Models;

namespace MantenimientoEstudiantes.Data
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> opciones)
            : base(opciones) { }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Estudiante -> Tareas
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Estudiante)
                .WithMany(e => e.Tareas)
                .HasForeignKey(t => t.IdEstudiante);

            base.OnModelCreating(modelBuilder);
        }
    }
}