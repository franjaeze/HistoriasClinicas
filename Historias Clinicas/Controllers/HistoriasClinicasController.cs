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
    public class HistoriasClinicasController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public HistoriasClinicasController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: HistoriaClinicas
        public IActionResult Index()
        {
            return View( _context.HistoriasClinicas.ToListAsync());
        }

        // GET: HistoriaClinicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoriaClinicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historiaClinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }
            return View(historiaClinica);
        }

        // POST: HistoriaClinicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historiaClinica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaClinicaExists(historiaClinica.Id))
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
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriasClinicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // POST: HistoriaClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historiaClinica = await _context.HistoriasClinicas.FindAsync(id);
            _context.HistoriasClinicas.Remove(historiaClinica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaClinicaExists(int id)
        {
            return _context.HistoriasClinicas.Any(e => e.Id == id);
        }
    }
}
