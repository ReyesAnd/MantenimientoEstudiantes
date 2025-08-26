using Microsoft.AspNetCore.Mvc;
using MantenimientoEstudiantes.Models;
using MantenimientoEstudiantes.Services; 

namespace MantenimientoEstudiantes.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly IEstudiantesService _estudiantesService;

        public EstudiantesController(IEstudiantesService estudiantesService)
        {
            _estudiantesService = estudiantesService;
        }

        public async Task<IActionResult> Index()
        {
            var estudiantes = await _estudiantesService.GetAllAsync();
            return View(estudiantes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var estudiante = await _estudiantesService.GetByIdAsync(id.Value);
            if (estudiante == null) return NotFound();
            return View(estudiante);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matricula,Nombre,FechaNacimiento")] Estudiante estudiante)
        {
            if (await _estudiantesService.MatriculaExistsAsync(estudiante.Matricula))
            {
                ModelState.AddModelError("Matricula", "Esta matrícula ya existe.");
            }

            if (ModelState.IsValid)
            {
                await _estudiantesService.CreateAsync(estudiante);
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var estudiante = await _estudiantesService.GetByIdAsync(id.Value);
            if (estudiante == null) return NotFound();
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Nombre,FechaNacimiento")] Estudiante estudiante)
        {
            if (id != estudiante.Id) return NotFound();

            if (await _estudiantesService.MatriculaExistsAsync(estudiante.Matricula, estudiante.Id))
            {
                ModelState.AddModelError("Matricula", "Esta matrícula ya existe.");
            }

            if (ModelState.IsValid)
            {
                await _estudiantesService.UpdateAsync(estudiante);
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var estudiante = await _estudiantesService.GetByIdAsync(id.Value);
            if (estudiante == null) return NotFound();
            return View(estudiante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _estudiantesService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> VerificarMatricula(string matricula)
        {
            var existe = await _estudiantesService.MatriculaExistsAsync(matricula);
            return Json(existe);
        }
    }
}