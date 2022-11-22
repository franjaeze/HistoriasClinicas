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

            return View(evolucion);
        }

        // GET: Evolucions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evolucions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,DescripcionAtencion")] Evolucion evolucion)
        {
            if (ModelState.IsValid)
            {
                evolucion.EpisodioId = id;
                evolucion.MedicoId = getUsuarioId();
                evolucion.FechaYHoraInicio = DateTime.Today;
                evolucion.EstadoAbierto = true;

                evolucion.Id= 0;
                _context.Add(evolucion);

                _context.SaveChanges();
               
                return RedirectToAction(nameof(Index));
                //return RedirectToAction(nameof(EvolucionesPorEpisodio));
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
            return View(evolucion);
        }

        // POST: Evolucions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,MedicoId,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,DescripcionAtencion,Indicaciones,PrecisaEstudiosAdicionales,PrecisaInterconsultaMedica")] Evolucion evolucion)
        {
            if (id != evolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evolucion);
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
                return RedirectToAction(nameof(Index));
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

        public IActionResult Cerrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.EvolucionId = id;

            var evolucion = _context.Evoluciones
                .FirstOrDefault(m => m.Id == id);
            if (evolucion == null)
            {
                return NotFound();
            }

            return View(evolucion);
        }

        public IActionResult CerrarEvolucion(int? id)
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


            return RedirectToAction(nameof(Index));
            
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

            var evoluciones = _context.Evoluciones.Where(x => x.EpisodioId == episodio.Id);

            ViewData["EpisodioId"] = id;

            return View(evoluciones);
        }


    }
}
