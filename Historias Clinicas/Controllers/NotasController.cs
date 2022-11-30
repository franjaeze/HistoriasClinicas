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
        public IActionResult Details(int id)
        {
            if (id == 0)
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

            var empleado = _context.Personas.Find(nota.EmpleadoId);

            ViewBag.EmpleadoNombre = empleado.NombreCompleto;
            TempData["evolucionId"] = nota.EvolucionId;

            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
            var episodio = _context.Episodios.Find(evolucion.EpisodioId);
            var historiaClinica = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);
            TempData["historiaClinicaId"] = historiaClinica.Id;

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create(int id)
        {
            
            var evolucion = _context.Evoluciones.Find(id);
            var episodio = _context.Episodios.Find(evolucion.EpisodioId);
            TempData["episodioId"] = episodio.Id;
            var historia = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);

            TempData["historiaClinicaId"] = historia.Id;
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
                nota.EmpleadoId = GetUsuarioId();
                nota.FechaYHora = DateTime.Now;
                nota.Id = 0;

                _context.Add(nota);
                _context.SaveChanges();
                ViewData["evolucionId"] = nota.EvolucionId;
                var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
                var episodio = _context.Episodios.Find(evolucion.EpisodioId);
                var hca = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);
                TempData["PacienteId"] = hca.PacienteId;

                return RedirectToAction("NotasPorEvolucion", "Notas" , new { id = @ViewData["evolucionId"] , historiaClinicaId = episodio.HistoriaClinicaId});
            }
            return View(nota);
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

            TempData["EvolucionId"] = nota.EvolucionId;
            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
            var episodio = _context.Episodios.FirstOrDefault(e => e.Id == evolucion.EpisodioId);
            var historiaClinica = _context.HistoriasClinicas.FirstOrDefault(e => e.Id == episodio.HistoriaClinicaId);
            TempData["HistoriaClinicaId"] = historiaClinica.Id;

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
                var notaEnDb = _context.Notas.FirstOrDefault(e => e.Id == id);
                try
                {

                    if (notaEnDb == null)
                    {
                        return NotFound();

                    }

                    notaEnDb.Mensaje = nota.Mensaje;
                    _context.SaveChanges();
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
                TempData["EvolucionId"] = notaEnDb.EvolucionId;
                var evolucion = _context.Evoluciones.Find(notaEnDb.EvolucionId);
                var episodio = _context.Episodios.FirstOrDefault(e => e.Id == evolucion.EpisodioId);
                var historiaClinica = _context.HistoriasClinicas.FirstOrDefault(e => e.Id == episodio.HistoriaClinicaId);
                TempData["HistoriaClinicaId"] = historiaClinica.Id;

                return RedirectToAction("NotasPorEvolucion", "Notas" , new { id = @TempData["EvolucionId"], historiaClinicaId = @TempData["HistoriaClinicaId"] });
            }
            return View(nota);
        }

        // GET: Notas/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
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

            var historiaClinica = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);

            TempData["evolucionId"] = evolucion.Id;
            TempData["historiaClinicaId"] = historiaClinica.Id;
            var empleado = _context.Personas.Find(nota.EmpleadoId);

            ViewBag.EmpleadoNombre = empleado.NombreCompleto;

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           
            var nota = _context.Notas.Find(id);
            var evolucion = _context.Evoluciones.Find(nota.EvolucionId);
            var episodio = _context.Episodios.Find(evolucion.EpisodioId);

            _context.Notas.Remove(nota);
            _context.SaveChanges();
            return RedirectToAction("NotasPorEvolucion", "Notas", new { id = nota.EvolucionId, historiaClinicaId = episodio.HistoriaClinicaId});
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.Id == id);
        }

        private int GetUsuarioId()
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


        public IActionResult NotasPorEvolucion(int id, int historiaClinicaId)
        {
            var evolucion = _context.Evoluciones.Find(id);

            var notas = _context.Notas
                .Where(x => x.EvolucionId == evolucion.Id);
            var hca = _context.HistoriasClinicas.Find(historiaClinicaId);

            ViewData["Estado"] = evolucion.EstadoAbierto;
            ViewData["evolucionId"] = id;
            TempData["EpisodioId"] = evolucion.EpisodioId;
            TempData["historiaId"] = historiaClinicaId;
            TempData["PacienteId"] = hca.PacienteId;

            return View(notas);
        }      
    }
}
