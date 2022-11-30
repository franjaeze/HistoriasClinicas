using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using Historias_Clinicas.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;
        public List<MedicoPaciente> MedicoPacientes;

        public MedicosController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Medicos
        public IActionResult Index()
        {
            // MedicosCollections();
            return View(_context.Medicos.ToList());
            //return View();
        }

        // GET: Medicos Menu de Opciones
        public IActionResult MenuMedico()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
            {
                Medico medicoEnDb = _context.Medicos.FirstOrDefault(c => c.NormalizedUserName == User.Identity.Name.ToUpper());
                if (medicoEnDb != null)
                {
                    ViewBag.Nombre = medicoEnDb.Nombre;
                    ViewBag.Id = medicoEnDb.Id;

                }
            }
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
                .Include(clt => clt.Direccion)
                .FirstOrDefault(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        [Authorize(Roles = "Empleado")]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatriculaNacional,Especialidad,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
        {

            VerificarDni(medico);

            if (ModelState.IsValid)
            {
                medico.UserName = medico.Email;
                medico.FechaDeAlta = DateTime.Now;
                var resultadoNewPersona = await _userManager.CreateAsync(medico, Configs.PasswordGenerica);

                if (resultadoNewPersona.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medico, Configs.MedicoRolName);
                    return RedirectToAction("Create", "Direcciones", new { id = medico.Id });

                }
                foreach (var error in resultadoNewPersona.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

                //List<MedicoPaciente> MedicoPacientes = new List<MedicoPaciente>();
                _context.SaveChanges();

                _context.Medicos.Add(medico);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create", "Direcciones", new { id = medico.Id });
            }
            return View(medico);
        }

        private bool DniExist(Medico medico)
        {
            bool devolver = false;
            if (medico.Dni != 0)
            {
                if (medico.Id != 0)
                {
                    devolver = _context.Personas.Any(p => p.Dni == medico.Dni && p.Id != medico.Id);
                }
                else
                {
                    devolver = _context.Personas.Any(p => p.Dni == medico.Dni);
                }
            }
            return devolver;
        }

        private void VerificarDni(Medico medico)
        {
            if (DniExist(medico))
            {
                ModelState.AddModelError("Dni", "Ya existe un persona con el dni ingresado");
            }
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
        public IActionResult Edit(int id, [Bind("MatriculaNacional,Especialidad,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            VerificarDni(medico);

            if (ModelState.IsValid)
            {
                try
                {
                    var medicoEnDb = _context.Medicos
                      .Include(m => m.Direccion)
                      .FirstOrDefault(m => m.Id == id);

                    if (medicoEnDb == null)
                    {
                        return NotFound();
                    }

                    medicoEnDb.Nombre = medico.Nombre;
                    medicoEnDb.SegundoNombre = medico.SegundoNombre;
                    medicoEnDb.Apellido = medico.Apellido;
                    medicoEnDb.Dni = medico.Dni;
                    medicoEnDb.Telefono = medico.Telefono;
                    medicoEnDb.MatriculaNacional = medico.MatriculaNacional;
                    medicoEnDb.Especialidad = medico.Especialidad;



                    _context.Update(medicoEnDb);
                    _context.SaveChanges();

                    if (medicoEnDb.Direccion == null)
                    {
                        return RedirectToAction("Create", "Direcciones", new { id = medico.Id });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "Direcciones", new { id = medicoEnDb.Direccion.Id });
                    }
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
                catch (DbUpdateException dbex)
                {
                    ProcesarDuplicado(dbex);
                }
            }
            return View(medico);
        }

        // GET: Medicos/Delete/5

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

        private void ProcesarDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("MatriculaNacional", MensajeError.MatriculaNacionalExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Buscar(string apellido)
        {
            var medicos = from m in _context.Medicos
                          select m;

            if (!String.IsNullOrEmpty(apellido))
            {
                medicos = medicos.Where(m => m.Apellido.Contains(apellido));
                ViewBag.Apellido = apellido;
            }

            return View(medicos);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ListarEspecialidad(Especialidad especialidad)
        {
            var lista = _context.Medicos.Where(m => m.Especialidad == especialidad);
            return View(lista);
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


        public IActionResult ListarPacientes()
        {
            int id = GetUsuarioId();
            var medico = _context.Medicos.Find(id);
            ViewData["MatriculaNacional"] = medico.MatriculaNacional;
            var medicosPacientes = _context.MedicoPaciente
                        .Where(x => x.MedicoId == medico.Id);
            var pacientes = _context.Pacientes.Where(x => medicosPacientes.Any(y => y.PacienteId == x.Id));

            return View(pacientes);
        }
    }
}