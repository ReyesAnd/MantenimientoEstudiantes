//using System.Diagnostics;
//using MantenimientoEstudiantes.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace MantenimientoEstudiantes.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using MantenimientoEstudiantes.Data;
using MantenimientoEstudiantes.Models;
using System.Linq;

namespace MantenimientoEstudiantes.Controllers
{
    public class HomeController : Controller
    {
        private readonly AplicacionDbContext _context;

        public HomeController(AplicacionDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var estudiantesConNotas = _context.Estudiantes
                .Select(e => new EstudianteViewModel
                {
                    Matricula = e.Matricula,
                    Nombre = e.Nombre,
                    Promedio = e.Tareas.Any()
                        ? e.Tareas.Average(t => t.Calificacion)
                        : 0,
                })
                .ToList();

            // asignar literal
            foreach (var est in estudiantesConNotas)
            {
                if (est.Promedio >= 90)
                    est.Literal = "A";
                else if (est.Promedio >= 80)
                    est.Literal = "B";
                else if (est.Promedio >= 70)
                    est.Literal = "C";
                else
                    est.Literal = "F";
            }

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
