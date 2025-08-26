using MantenimientoEstudiantes.Controllers;
using MantenimientoEstudiantes.Models;

namespace MantenimientoEstudiantes.Services
{
    public interface IEstudiantesService
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task CreateAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(int id);
        Task<bool> MatriculaExistsAsync(string matricula, int id = 0);
        Task<IEnumerable<EstudianteViewModel>> GetEstudiantesConPromedioAsync();
    }
}