using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Entity_Framework.Data;
using MVC_Entity_Framework.Models;

namespace MVC_Entity_Framework.Controllers
{
    [Authorize]
    public class EstudiantesController : Controller
    {
        private readonly MVC_Entity_FrameworkContext _context;

        public EstudiantesController(MVC_Entity_FrameworkContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estudiantes.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }



        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                estudiante.Id = Guid.NewGuid();
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Dni,Nombre,Apellido,FechaDeNacimiento")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            return View(estudiante);
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(Guid id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }


        public IActionResult Materias(Guid id)
        {
            if (!EstudianteExists(id))
            {
                return NotFound();
            }

            var estudianteConMaterias = _context.Estudiantes               
                                            .Include(estudiante => estudiante.Materias)
                                                .ThenInclude(materiaAlumno => materiaAlumno.Materia)
                                            .FirstOrDefault(e => e.Id == id);

            var materias = estudianteConMaterias.Materias.Select(matEst => matEst.Materia);

			ViewData["EstudianteId"] = id;
            return View(materias);
        }

    }
}
