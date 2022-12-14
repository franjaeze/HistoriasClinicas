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
    public class DireccionesController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public DireccionesController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Direcciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Direcciones.ToListAsync());
        }

        // GET: Direcciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }


        // GET: Direcciones/Create
        public IActionResult Create(int? id)
        {
            ViewBag.PersonaId = id; // Puede ser persona,empleado,etc

            var medico = _context.Medicos.Find(id);

            if (medico == null)
            {
                var paciente = _context.Pacientes.Find(id);

                if (paciente != null)
                {
                    ViewData["esPaciente"] = paciente;
                }
                else
                {
                    var empleado = _context.Empleados.Find(id);
                    ViewData["esEmpleado"] = empleado;
                }
            }
            else
            {
                ViewData["esMedico"] = medico;
            }
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Id,PersonaId,Calle,Altura,Piso,Departamento,Localidad")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                direccion.PersonaId = id;
                direccion.Id = 0;
                _context.Direcciones.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(direccion);
        }

        // GET: Direcciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            return View(direccion);
        }

        // POST: Direcciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonaId,Calle,Altura,Piso,Departamento,Localidad")] Direccion direccion)
        {
            
            
            if (id != direccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var direccionEnDb = _context.Direcciones.FirstOrDefault(d => d.Id == id);

                    if (direccionEnDb == null)
                    {
                        return NotFound();

                    }

                    direccionEnDb.Calle = direccion.Calle;
                    direccionEnDb.Altura = direccion.Altura;
                    direccionEnDb.Piso = direccion.Piso;
                    direccionEnDb.Departamento = direccion.Departamento;
                    direccionEnDb.Localidad = direccion.Localidad;

                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if(User.IsInRole("Empleado"))
                {
                    return RedirectToAction("MenuEmpleado", "Empleados");
                }
                if(User.IsInRole("Paciente"))
                {
                    return RedirectToAction("MenuPaciente", "Pacientes");
                }
                if (User.IsInRole("Medico"))
                {
                    return RedirectToAction("MenuMedico", "Medicos");
                }

            }
            return View(direccion);
        }

        // GET: Direcciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direcciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
            return _context.Direcciones.Any(e => e.Id == id);
        }
    }
}
