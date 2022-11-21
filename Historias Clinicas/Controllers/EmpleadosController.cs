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
using Microsoft.Data.SqlClient;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class EmpleadosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EmpleadosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Empleadoes

        [Authorize(Roles = "Empleado")]
        public  IActionResult Index()
        {
            return View(_context.Empleados.ToList());
        }

        public IActionResult MenuEmpleado()
        {
            return View();
        }

        // GET: Empleadoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Empleados
                .Include(clt => clt.Direccion)
                .FirstOrDefault(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Legajo,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Empleado empleado)
        {

            VerificarDni(empleado);

            if (ModelState.IsValid)
            {
                _context.Add(empleado);

                try
                {
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbex)
                {
                    ProcesarDuplicado(dbex);
                }
            }
            return View(empleado);
        }

        private bool DniExist(Empleado empleado)
        {
            bool devolver = false;
            if (empleado.Dni != 0)
            {
                if (empleado.Id != 0)
                {
                    devolver = _context.Personas.Any(p => p.Dni == empleado.Dni && p.Id != empleado.Id);
                }
                else
                {
                    devolver = _context.Personas.Any(p => p.Dni == empleado.Dni);
                }
            }
            return devolver;
        }

        private void VerificarDni(Empleado empleado)
        {
            if (DniExist(empleado))
            {
                ModelState.AddModelError("Dni", "Ya existe una persona con el dni ingresado");
            }
        }

        // GET: Empleadoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado =_context.Empleados.Find(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Legajo,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            VerificarDni(empleado);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dbex)
                {
                    ProcesarDuplicado(dbex);
                }
            }
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
    
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = _context.Empleados
                .FirstOrDefault(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var empleado = _context.Empleados.Find(id);
            _context.Empleados.Remove(empleado);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }

        private void ProcesarDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("Legajo", MensajeError.LegajoExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }

        private IActionResult AltaEpisodio(int idPac)
        {
            var paciente = _context.Pacientes
                           .Find(idPac);
            var hca = _context.HistoriasClinicas
                       .Find(paciente.HistoriaClinicaId);

            if (hca != null)
            {
                Episodio episodio = new Episodio();
                hca.Episodios.Add(episodio);
                _context.SaveChanges();

                return RedirectToAction("Create", "Episodios", new { id = episodio.Id });
            }

            return View(paciente);
        }
    }
}
