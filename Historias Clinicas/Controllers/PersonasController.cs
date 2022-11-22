 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Data;
using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Historias_Clinicas.Controllers
{
    public class PersonasController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;

        public PersonasController(HistoriasClinicasContext context, UserManager<Persona> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Personas
       
        public IActionResult Index()
        {
            return View(_context.Personas.ToList());
        }

        // GET: Personas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _context.Personas
                .Include(clt => clt.Direccion)
                .FirstOrDefault(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(bool EsMedico, bool EsEmpleado, bool EsPaciente,[Bind("Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Persona persona)
        {

            VerificarDni(persona);

            if (ModelState.IsValid)
            {
                try
                {
                    persona.UserName = persona.Email;
                    var resultadoNewPersona = await _userManager.CreateAsync(persona, Configs.PasswordGenerica);

                    if (resultadoNewPersona.Succeeded)
                    {
                        IdentityResult resultadoAddRole;
                        string rolDefinido;

                        if (EsMedico)
                        {
                            rolDefinido = Configs.MedicoRolName;
                        }
                        else if (EsEmpleado)
                        {
                            rolDefinido = Configs.EmpleadoRolName;
                        }
                        else
                        {
                            rolDefinido = Configs.PacienteRolName;
                        }

                        resultadoAddRole = await _userManager.AddToRoleAsync(persona, rolDefinido);

                        if (resultadoAddRole.Succeeded)
                        {
                            _context.Personas.Add(persona);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Create", "Direcciones", new { id = persona.Id });
                            
                        }
                        else
                        {
                            return Content($"No se ha podido agregar el rol{rolDefinido}");
                        }
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
            return View(persona);
        }

        private bool DniExist(Persona persona)
        {
            bool devolver = false;
            if (persona.Dni != 0)
            {
                if (persona.Id != 0)
                {
                    devolver = _context.Personas.Any(p => p.Dni == persona.Dni && p.Id != persona.Id);
                }
                else
                {
                    devolver = _context.Personas.Any(p => p.Dni == persona.Dni);
                }
            }
            return devolver;
        }

        private void VerificarDni(Persona persona)
        {
            if (DniExist(persona))
            {
                ModelState.AddModelError("Dni", "Ya existe una persona con el dni ingresado");
            }
        }

        // GET: Personas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _context.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            VerificarDni(persona);

            if (ModelState.IsValid)
            {
                try
                {
                    var personaEnDb = _context.Pacientes.Find(persona.Id);
                    if (personaEnDb == null)
                    {
                        return NotFound();
                    }

                    personaEnDb.Nombre = persona.Nombre;
                    personaEnDb.SegundoNombre = persona.SegundoNombre;
                    personaEnDb.Apellido = persona.Apellido;
                    personaEnDb.Dni = persona.Dni;
                    personaEnDb.Email = persona.Email;
                    personaEnDb.Telefono = persona.Telefono;
                    personaEnDb.FechaDeAlta = persona.FechaDeAlta;

                    if(!ActualizarEmail(persona, personaEnDb))
                    {
                        ModelState.AddModelError("Email", "El email ya esta en uso");
                        return View(persona);
                    }
                    
                    _context.Update(personaEnDb);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
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
            return View(persona);
        }

        private bool ActualizarEmail(Persona personaForm, Persona personaDb)
        {
            bool resultado = true;
            try
            {
                if (!personaDb.NormalizedEmail.Equals(personaForm.Email.ToUpper()))
                {
                    if (ExistEmail(personaForm.Email))
                    {
                        resultado = false;
                    }
                    else
                    {
                        personaDb.Email = personaForm.Email;
                        personaDb.NormalizedEmail = personaForm.Email.ToUpper();
                        personaDb.UserName = personaForm.Email;
                        personaDb.NormalizedUserName = personaForm.NormalizedEmail;

                    }
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }

        private bool ExistEmail(string email)
        {
            return _context.Personas.Any(p => p.NormalizedEmail == email.ToUpper());
        }

        // GET: Personas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _context.Personas
                .FirstOrDefault(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var persona = _context.Personas.Find(id);
            _context.Personas.Remove(persona);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
            return _context.Personas.Any(e => e.Id == id);
        }

        private void ProcesarDuplicado(DbUpdateException dbex)
        {
            SqlException innerException = dbex.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                ModelState.AddModelError("Dni", MensajeError.DniExistente);
            }
            else
            {
                ModelState.AddModelError(string.Empty, dbex.Message);
            }
        }
    }
}
