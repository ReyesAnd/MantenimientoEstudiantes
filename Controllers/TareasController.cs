using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MantenimientoEstudiantes.Data;
using MantenimientoEstudiantes.Models;
using MantenimientoEstudiantes.Services;

namespace MantenimientoEstudiantes.Controllers
{
    public class TareasController : Controller
    {
        private readonly ITareasService _tareasService;
        private readonly IEstudiantesService _estudiantesService;

        public TareasController(ITareasService tareasService, IEstudiantesService estudiantesService)
        {
            _tareasService = tareasService;
            _estudiantesService = estudiantesService;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            return View(await _tareasService.GetAllConEstudianteAsync());
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["IdEstudiante"] = new SelectList(await _estudiantesService.GetAllAsync(), "Id", "Matricula");
            return View();
        }

        // GET: Tareas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdEstudiante"] = new SelectList(await _estudiantesService.GetAllAsync(), "Id", "Matricula");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdTarea,IdEstudiante,Descripcion,Calificacion")] Tarea tarea)
        public async Task<IActionResult> Create([Bind("IdEstudiante,Descripcion,Calificacion")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                await _tareasService.CreateAsync(tarea);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(await _estudiantesService.GetAllAsync(), "Id", "Matricula", tarea.IdEstudiante);
            return View(tarea);
        }

        [HttpGet]
        public async Task<JsonResult> GetNombreEstudiante(int idEstudiante)
        {
            var nombre = await _tareasService.GetNombreEstudianteAsync(idEstudiante);
            return Json(nombre);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _tareasService.GetByIdAsync(id.Value);

            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(await _estudiantesService.GetAllAsync(), "Id", "Matricula", tarea.IdEstudiante);

            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,IdEstudiante,Descripcion,Calificacion")] Tarea tarea)
        {
            if (id != tarea.IdTarea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tareasService.UpdateAsync(tarea);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TareaExists(tarea.IdTarea))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(await _estudiantesService.GetAllAsync(), "Id", "Matricula", tarea.IdEstudiante);

            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _tareasService.GetByIdConEstudianteAsync(id.Value);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tareasService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TareaExists(int id)
        {
            var tarea = await _tareasService.GetByIdAsync(id);
            return tarea != null;
        }

    }
}
