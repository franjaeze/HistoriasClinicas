﻿using System;
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
    public class EvolucionesController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EvolucionesController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Evolucions
        public IActionResult Index()
        {
            return View(_context.Evoluciones.ToList());
        }

        // GET: Evolucions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = _context.Evoluciones
                .FirstOrDefault(m => m.Id == id);
            if (evolucion == null)
            {
                return Content($"La evolucion con id {id} no fue encontrada");
                // Se cambio del NotFound para que no se rompa todo
            }

            var medico = _context.Medicos
               .FirstOrDefault(m => m.Id == 5);

            var episodioId = evolucion.EpisodioId;
            TempData["EpisodioId"] = episodioId;
            TempData["nombreMedico"] = medico.NombreCompleto;

            return View(evolucion);
        }

        // GET: Evolucions/Create
        public IActionResult Create(int id)
        {
            @TempData["EpisodioId"] = id;
            return View();
        }

        // POST: Evolucions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,EpisodioId,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,DescripcionAtencion")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                evolucion.EpisodioId = id;
                TempData["EpisodioId"] = id;
                evolucion.MedicoId = GetUsuarioId();
                evolucion.FechaYHoraInicio = DateTime.Now;
                evolucion.EstadoAbierto = true;

                evolucion.Id = 0;
                _context.Add(evolucion);

                _context.SaveChanges();


                return RedirectToAction("EvolucionesPorEpisodio", "Evoluciones", new { id = @TempData["EpisodioId"] });
            }
            return View(evolucion);
        }

        // GET: Evolucions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = _context.Evoluciones.Find(id);

            if (evolucion == null)
            {
                return NotFound();
            }

            var episodioId = evolucion.EpisodioId;
            TempData["EpisodioId"] = episodioId;
            var episodio = _context.Episodios.Find(episodioId);
            TempData["historiaId"] = episodio.HistoriaClinicaId;

            return View(evolucion);
        }

        // POST: Evolucions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EpisodioId,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,DescripcionAtencion")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var evolucionEnDb = _context.Evoluciones.FirstOrDefault(e => e.Id == id);

                    if (evolucionEnDb == null)
                    {
                        return NotFound();

                    }

                    evolucionEnDb.DescripcionAtencion = evolucion.DescripcionAtencion;
                    evolucionEnDb.FechaYHoraAlta = evolucion.FechaYHoraAlta;

                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvolucionExists(evolucion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("EvolucionesPorEpisodio", "Evoluciones", new { ruta = @TempData["EpisodioId"] });
            }
            return View(evolucion);
        }

        // GET: Evolucions/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucion = _context.Evoluciones
                .FirstOrDefault(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        // POST: Evolucions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var evolucion = _context.Evoluciones.Find(id);
            _context.Evoluciones.Remove(evolucion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EvolucionExists(int id)
        {
            return _context.Evoluciones.Any(e => e.Id == id);
        }

        public IActionResult Cerrar(int id, int paciente)
        {
            var evolucion = _context.Evoluciones
                .FirstOrDefault(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }
            TempData["paciente"] = paciente;
            TempData["EpisodioId"] = id;
            return View(evolucion);
        }

        public IActionResult CerrarEvolucion(int? id, int historiaId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evolucionb = _context.Evoluciones.Find(id);

            if (evolucionb == null)
            {
                return NotFound();
            }
            evolucionb.EstadoAbierto = false;
            _context.SaveChanges();
            evolucionb.FechaYHoraCierre = DateTime.Now;
            _context.SaveChanges();

            TempData["historiaId"] = historiaId;

            var episodio = _context.Episodios.Find(evolucionb.EpisodioId);
            var hca = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);
            TempData["EpisodioId"] = episodio.Id;
            TempData["PacienteId"] = hca.PacienteId;

            return RedirectToAction("EvolucionesPorEpisodio", "Evoluciones", new { id = @TempData["EpisodioId"] });
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



        public IActionResult EvolucionesPorEpisodio(int id, int paciente)
        {
            var episodio = _context.Episodios.Find(id);

            var evoluciones = _context.Evoluciones.Where(x => x.EpisodioId == episodio.Id);

            var hca = _context.HistoriasClinicas.Find(episodio.HistoriaClinicaId);

            int pacienteId = hca.PacienteId;

            TempData["EpisodioId"] = id;
            TempData["historiaId"] = paciente;
            TempData["PacienteId"] = pacienteId;
            return View(evoluciones);
        }
    }
}
