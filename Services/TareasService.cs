using MantenimientoEstudiantes.Data;
using MantenimientoEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace MantenimientoEstudiantes.Services
{
    public class TareasService : ITareasService
    {
        private readonly AplicacionDbContext _context;

        public TareasService(AplicacionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllConEstudianteAsync()
        {
            return await _context.Tareas.Include(t => t.Estudiante).ToListAsync();
        }

        public async Task<Tarea?> GetByIdConEstudianteAsync(int id)
        {
            return await _context.Tareas
                .Include(t => t.Estudiante)
                .FirstOrDefaultAsync(m => m.IdTarea == id);
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task CreateAsync(Tarea tarea)
        {
            _context.Add(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tarea tarea)
        {
            _context.Update(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tarea = await GetByIdAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string?> GetNombreEstudianteAsync(int idEstudiante)
        {
            return await _context.Estudiantes
                .Where(e => e.Id == idEstudiante)
                .Select(e => e.Nombre)
                .FirstOrDefaultAsync();
        }
    }
}