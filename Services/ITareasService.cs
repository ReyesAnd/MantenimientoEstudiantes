using MantenimientoEstudiantes.Models;

namespace MantenimientoEstudiantes.Services
{
    public interface ITareasService
    {
        Task<IEnumerable<Tarea>> GetAllConEstudianteAsync();
        Task<Tarea?> GetByIdConEstudianteAsync(int id);
        Task<Tarea?> GetByIdAsync(int id);
        Task CreateAsync(Tarea tarea);
        Task UpdateAsync(Tarea tarea);
        Task DeleteAsync(int id);
        Task<string?> GetNombreEstudianteAsync(int idEstudiante);
    }
}