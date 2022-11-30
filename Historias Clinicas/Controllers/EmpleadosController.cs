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
using Historias_Clinicas.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class EmpleadosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public EmpleadosController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Empleadoes

        [Authorize(Roles = "Empleado")]
        public  IActionResult Index()
        {
            return View(_context.Empleados.ToList());
        }

        public IActionResult MenuEmpleado()
        {

            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                Empleado empleadoEnDb = _context.Empleados.FirstOrDefault(c => c.NormalizedUserName == User.Identity.Name.ToUpper());
                if (empleadoEnDb != null)
                {
                    ViewBag.Nombre = empleadoEnDb.Nombre;
                    ViewBag.Id = empleadoEnDb.Id;

                }
            }
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
        public async Task<IActionResult> Create([Bind("Id,Legajo,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Empleado empleado)
        {

            VerificarDni(empleado);

            if (ModelState.IsValid)
            {
                try
                {
                    empleado.UserName = empleado.Email;
                    empleado.FechaDeAlta = DateTime.Now;
                    var resultadoNewPersona = await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);

                    if (resultadoNewPersona.Succeeded)
                    { 
                        await _userManager.AddToRoleAsync(empleado, Configs.EmpleadoRolName);

                        return RedirectToAction("Create", "Direcciones", new { id = empleado.Id });
                    }
                    foreach (var error in resultadoNewPersona.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
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

                    var empleadoEnDb = _context.Empleados
                       .Include(e => e.Direccion)
                       .FirstOrDefault(e => e.Id == id);

                    if (empleadoEnDb == null)
                    {
                        return NotFound();
                    }

                    empleadoEnDb.Nombre = empleado.Nombre;
                    empleadoEnDb.SegundoNombre = empleado.SegundoNombre;
                    empleadoEnDb.Apellido = empleado.Apellido;
                    empleadoEnDb.Dni = empleado.Dni;
                    empleadoEnDb.Telefono = empleado.Telefono;
                    empleadoEnDb.Legajo = empleado.Legajo;

                    _context.Update(empleadoEnDb);
                    _context.SaveChanges();

                    if (empleadoEnDb.Direccion == null)
                    {
                        return RedirectToAction("Create", "Direcciones", new { id = empleado.Id });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "Direcciones", new { id = empleadoEnDb.Direccion.Id });
                    }
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
        public IActionResult Buscar(string apellido)
        {
            var empleados = from m in _context.Empleados
                          select m;

            if (!String.IsNullOrEmpty(apellido))
            {
                empleados = empleados.Where(m => m.Apellido.Contains(apellido));
                ViewBag.Apellido = apellido;
            }

            return View(empleados);
        }
    }
}
