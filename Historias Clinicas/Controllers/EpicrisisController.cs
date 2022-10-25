using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.Controllers
{
    public class EpicrisisController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpicrisisController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Epicrisis
        public IActionResult Index()
        {
            return View(_context.Epicrisis.ToList());
        }

        // GET: Epicrisis/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = _context.Epicrisis
                .FirstOrDefault(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // GET: Epicrisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Epicrisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MedicoId,PacienteId,Resumen,DiasInternacion,FechaYHora,FechaYHoraAlta,FechaYHoraIngreso")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(epicrisis);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(epicrisis);
        }

        // GET: Epicrisis/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = _context.Epicrisis.Find(id);
            if (epicrisis == null)
            {
                return NotFound();
            }
            return View(epicrisis);
        }

        // POST: Epicrisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,MedicoId,PacienteId,Resumen,DiasInternacion,FechaYHora,FechaYHoraAlta,FechaYHoraIngreso")] Epicrisis epicrisis)
        {
            if (id != epicrisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(epicrisis);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpicrisisExists(epicrisis.Id))
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
            return View(epicrisis);
        }

        // GET: Epicrisis/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var epicrisis = _context.Epicrisis
                .FirstOrDefault(m => m.Id == id);
            if (epicrisis == null)
            {
                return NotFound();
            }

            return View(epicrisis);
        }

        // POST: Epicrisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var epicrisis = _context.Epicrisis.Find(id);
            _context.Epicrisis.Remove(epicrisis);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EpicrisisExists(int id)
        {
            return _context.Epicrisis.Any(e => e.Id == id);
        }
    }
}
