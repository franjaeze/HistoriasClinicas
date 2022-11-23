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
        public IActionResult Create(int id, [Bind("Id,MedicoId,FechaYHora, Diagnostico")] Epicrisis epicrisis)
        {
            if (ModelState.IsValid)
            {

                epicrisis.EpisodioId = id;
                epicrisis.MedicoId = GetUsuarioId();
                epicrisis.FechaYHora = DateTime.Today;

                epicrisis.Id = 0;
                _context.Add(epicrisis);

                _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("CargarDiagnostico",new { id = epicrisis.Id });
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


        public IActionResult EpicrisisPorEpisodio(int id)
        {
            Episodio episodio = _context.Episodios.Find(id);

            var epicrisis = _context.Epicrisis
                .Where(x => x.EpisodioId == episodio.Id);

            ViewData["episodioId"] = id;

            return View(epicrisis);
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

        public IActionResult CargarDiagnostico(int id)
        {
            var epicrisis = _context.Epicrisis.Find(id);

            if (epicrisis.Diagnostico == null)
            {

                return RedirectToAction("Create", "Diagnosticos", new { id = epicrisis.Id });
            }

            return View(epicrisis);
        }
    }
}
