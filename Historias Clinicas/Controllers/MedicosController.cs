using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class MedicosController : Controller
    {
        private readonly ILogger<MedicosController> _logger;
        private readonly HistoriasClinicasContext _context;
        IList<Medico> _listaMedicos;

        public MedicosController(ILogger<MedicosController> logger, HistoriasClinicasContext context)
        {
            _logger = logger;
            this._context = context;
            
        }

        // GET: Medicos
        public IActionResult Index(String mensaje)
        {
            MedicosCollections();
            return View(_context.Medicos.ToList());
            //return View();
        }

        private void MedicosCollections()
        {
            IList<Medico> listaMedicos = _context.Medicos.ToList();

            foreach (Medico medico in listaMedicos)
            {
                _logger.Log(LogLevel.Information, $"IList - {medico.MatriculaNacional} {medico.Nombre} {medico.Apellido} {medico.Especialidad} {medico.Email} {medico.FechaDeAlta}");
            }
        }

        // GET: Medicos Menu de Opciones
        public IActionResult Menu()
        {
            return View();
        }

        // GET: Medicos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = _context.Medicos
                .FirstOrDefault(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        [Authorize(Roles = "Admin, Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("MatriculaNacional,Especialidad,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                _context.SaveChanges();
                _listaMedicos.Add(medico);
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = _context.Medicos.Find(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("MatriculaNacional,Especialidad,EstaActivo,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id))
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
            return View(medico);
        }

        // GET: Medicos/Delete/5
        [Authorize(Roles = "Admin, Empleado")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = _context.Medicos
                .FirstOrDefault(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            _listaMedicos.Remove(medico);
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var medico = _context.Medicos.Find(id);
            _context.Medicos.Remove(medico);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}
   



