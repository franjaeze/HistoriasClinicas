using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpisodiosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public IActionResult Index()
        {
            return View(_context.Episodios.ToList());
        }

        // GET: Episodios/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios
                .FirstOrDefault(m => m.Id == id);
            if (episodio == null)
            {
                return Content($"El episodio con id {id} no fue encontrado");
                // Se cambio del NotFound para que no se rompa todo
            }

            return View(episodio);
        }

        // GET: Episodios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,HistoriaClinicaId,EpicrisisId,Descripcion,Motivo,Internacion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,Especialidad")] Episodio episodio)
        {
            if (ModelState.IsValid)
                        
            {
                episodio.EmpleadoId = getUsuarioId();
                
                var paciente = _context.Pacientes.Find(id);
                var historia = _context.HistoriasClinicas.Find(paciente.HistoriaClinicaId);
                episodio.HistoriaClinicaId = historia.Id;

                episodio.Id = 0;
                episodio.FechaYHoraInicio = DateTime.Today;
                episodio.EstadoAbierto = true;
                _context.Add(episodio);
                _context.SaveChanges();

                historia.Episodios.Add(episodio);
                _context.SaveChanges();


                List<Evolucion> Evoluciones = new List<Evolucion>();
                return RedirectToAction(nameof(Index));
            }
            return View(episodio);
        }

        // GET: Episodios/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios.Find(id);
            if (episodio == null)
            {
                return NotFound();
            }
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PacienteId,MedicoId,Descripcion,Motivo,Antecedentes,Internacion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,Especialidad")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
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
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios
                .FirstOrDefault(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var episodio = _context.Episodios.Find(id);
            _context.Episodios.Remove(episodio);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
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

        public IActionResult EvolucionesPorEpisodio(int id)
        {
            var episodio = _context.Episodios.Find(id);
                       
            var evoluciones = _context.Evoluciones
                .Where(x => x.EpisodioId == episodio.Id);

            ViewData["episodioId"] = id;

            return View(evoluciones);
        }

        public IActionResult CargarEvolucion(int id)
        {
            var episodio = _context.Episodios.Find(id);

            return RedirectToAction("Create", "Evoluciones", new { id = episodio.Id });
        }



    }

}
