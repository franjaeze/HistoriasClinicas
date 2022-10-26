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
    public class AppsController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public AppsController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Apps
        public IActionResult Index()
        {
            return View(_context.App.ToList());
        }

        // GET: Apps/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _context.App
                .FirstOrDefault(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // GET: Apps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nombre,Direccion,Telefono")] App app)
        {
            if (ModelState.IsValid)
            {
                _context.Add(app);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(app);
        }

        // GET: Apps/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _context.App.Find(id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        // POST: Apps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Direccion,Telefono")] App app)
        {
            if (id != app.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(app);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppExists(app.Id))
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
            return View(app);
        }

        // GET: Apps/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var app = _context.App
                .FirstOrDefault(m => m.Id == id);
            if (app == null)
            {
                return NotFound();
            }

            return View(app);
        }

        // POST: Apps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var app = _context.App.Find(id);
            _context.App.Remove(app);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool AppExists(int id)
        {
            return _context.App.Any(e => e.Id == id);
        }
    }
}
