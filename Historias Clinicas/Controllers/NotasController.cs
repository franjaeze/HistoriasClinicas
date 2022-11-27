using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using System.Security.Claims;

namespace Historias_Clinicas.Controllers
{
    public class NotasController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public NotasController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Notas
        public IActionResult Index()
        {
            return View(_context.Notas.ToList());
        }

        // GET: Notas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = _context.Notas
                .FirstOrDefault(m => m.Id == id);
            if (nota == null)
            {
                return Content($"La nota con id {id} no fue encontrada");
                // Se cambio del NotFound para que no se rompa todo
            }

            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == nota.EmpleadoId);

            ViewBag.EmpleadoNombre = empleado.NombreCompleto;
            TempData["evolucionId"] = nota.EvolucionId;

            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
            var episodio = _context.Episodios.Find(evolucion.EpisodioId);
            var historiaClinica = _context.Episodios.Find(episodio.HistoriaClinicaId);
            TempData["historiaClinicaId"] = historiaClinica.Id;

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,MedicoID,Mensaje,FechaYHora")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                nota.EvolucionId = id;
                nota.EmpleadoId = getUsuarioId();
                nota.FechaYHora = DateTime.Now;
                nota.Id = 0;

                _context.Add(nota);
                _context.SaveChanges();
                ViewData["evolucionId"] = nota.EvolucionId;
                return RedirectToAction("NotasPorEvolucion", "Notas" , new { id = @ViewData["evolucionId"]});
            }
            return RedirectToAction("NotasPorEvolucion", "Notas", new { id = @ViewData["evolucionId"] });
        }

        // GET: Notas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = _context.Notas.Find(id);
            if (nota == null)
            {
                return NotFound();
            }
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,MedicoID,Mensaje,FechaYHora")] Nota nota)
        {
            if (id != nota.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(nota.EmpleadoId == getUsuarioId()) 
                    { 
                    _context.Update(nota);
                    _context.SaveChanges();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Pacientes");
            }
            return View(nota);
        }

        // GET: Notas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = _context.Notas.FirstOrDefault(m => m.Id == id);

            if (nota == null)
            {
                return NotFound();
            }

            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);

            var episodio = _context.Episodios.Find(evolucion.EpisodioId);

            var historiaClinica = _context.Episodios.Find(episodio.HistoriaClinicaId);

            TempData["evolucionId"] = evolucion.Id;
            TempData["historiaClinicaId"] = historiaClinica.Id;

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nota = _context.Notas.Find(id);
            _context.Notas.Remove(nota);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }

        private int getUsuarioId()
        {
            var userIdValue = 0;
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                                  .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = Int32.Parse(userIdClaim.Value);
                }
            }

            return userIdValue;
        }


        public IActionResult NotasPorEvolucion(int id, int? paciente)
        {
            var evolucion = _context.Evoluciones.Find(id);

            var notas = _context.Notas
                .Where(x => x.EvolucionId == evolucion.Id);
            var hca = _context.HistoriasClinicas.Find(paciente);

            ViewData["Estado"] = evolucion.EstadoAbierto;
            ViewData["evolucionId"] = id;
            TempData["historiaId"] = paciente;
            TempData["PacienteId"] = hca.PacienteId;

            return View(notas);
        }

        

    }
}
