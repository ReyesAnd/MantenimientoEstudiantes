using Microsoft.AspNetCore.Mvc;
using MantenimientoEstudiantes.Data;
using MantenimientoEstudiantes.Services;

namespace MantenimientoEstudiantes.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public HomeController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        public async Task<IActionResult> Index()
        {
            var estudiantesConNotas = await _estudiantesService.GetEstudiantesConPromedioAsync();
            return View(estudiantesConNotas);
        }
    }

    // ViewModel auxiliar
    public class EstudianteViewModel
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public decimal Promedio { get; set; }
        public string Literal { get; set; }
    }
}
