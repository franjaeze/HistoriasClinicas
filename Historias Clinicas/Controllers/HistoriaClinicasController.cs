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

    public class HistoriaClinicasController : Controller
    {
        private readonly HistoriasClinicasContext _context;
       

        public HistoriaClinicasController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: HistoriaClinicas
        public IActionResult Index()
        {
            return View(_context.HistoriasClinicas.ToList());
        }

        // GET: HistoriaClinicas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = _context.HistoriasClinicas
                .FirstOrDefault(m => m.Id == id);
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
        public IActionResult Create([Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historiaClinica);
                _context.SaveChanges();
                List<Episodio> Episodios = new List<Episodio>();
                return RedirectToAction(nameof(Index));
            }
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = _context.HistoriasClinicas.Find(id);
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
        public IActionResult Edit(int id, [Bind("Id,PacienteId")] HistoriaClinica historiaClinica)
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
                    _context.SaveChanges();
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
                @TempData["PacienteId"] = historiaClinica.PacienteId;
                return RedirectToAction(nameof(Index));
            }
            return View(historiaClinica);
        }

        // GET: HistoriaClinicas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = _context.HistoriasClinicas
                .FirstOrDefault(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // POST: HistoriaClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var historiaClinica = _context.HistoriasClinicas.Find(id);
            _context.HistoriasClinicas.Remove(historiaClinica);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaClinicaExists(int id)
        {
            return _context.HistoriasClinicas.Any(e => e.Id == id);
        }

        public IActionResult HistoriaClinicaDePaciente(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            var historia = _context.HistoriasClinicas.Find(paciente.HistoriaClinicaId);
            var episodios = _context.Episodios
                .Where(x => x.HistoriaClinicaId == historia.Id);
         
            TempData["historiaId"] = historia.Id;
            TempData["pacienteId"] = id;
            TempData["nombrePaciente"] = paciente.NombreCompleto;
            return View(episodios);
        }
    }
}
