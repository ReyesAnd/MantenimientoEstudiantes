using MantenimientoEstudiantes.Controllers;
using MantenimientoEstudiantes.Data;
using MantenimientoEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoEstudiantes.Services
{
    public class EstudiantesService : IEstudiantesService
    {
        private readonly AplicacionDbContext _context;

        public EstudiantesService(AplicacionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task CreateAsync(Estudiante estudiante)
        {
            _context.Add(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await GetByIdAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MatriculaExistsAsync(string matricula, int id = 0)
        {
            // Si el id es 0, estamos creando. Si no, estamos editando y debemos excluir al estudiante actual de la verificación.
            return await _context.Estudiantes.AnyAsync(e => e.Matricula == matricula && e.Id != id);
        }

        // Aquí movemos la lógica que estaba en HomeController
        public async Task<IEnumerable<EstudianteViewModel>> GetEstudiantesConPromedioAsync()
        {
            var estudiantesConNotas = await _context.Estudiantes
                .Select(e => new EstudianteViewModel
                {
                    Matricula = e.Matricula,
                    Nombre = e.Nombre,
                    Promedio = e.Tareas.Any() ? e.Tareas.Average(t => t.Calificacion) : 0,
                })
                .ToListAsync();

            // Asignar literal
            foreach (var est in estudiantesConNotas)
            {
                if (est.Promedio >= 90) est.Literal = "A";
                else if (est.Promedio >= 80) est.Literal = "B";
                else if (est.Promedio >= 70) est.Literal = "C";
                else est.Literal = "F";
            }

            return estudiantesConNotas;
        }
    }
}