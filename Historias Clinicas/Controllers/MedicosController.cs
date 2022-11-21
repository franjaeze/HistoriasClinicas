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

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        public List<MedicoPaciente> MedicoPacientes;

        public MedicosController(HistoriasClinicasContext context)
        {
   
            this._context = context;
            
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
        public IActionResult Create([Bind("MatriculaNacional,Especialidad,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
        {

            VerificarDni(medico);

            if (ModelState.IsValid)
            {
                _context.Add(medico);
                _context.SaveChanges();

                List<MedicoPaciente> MedicoPacientes = new List<MedicoPaciente>();


                this.MedicoPacientes = new List<MedicoPaciente>();

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
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
        public IActionResult Edit(int id, [Bind("MatriculaNacional,Especialidad,EstaActivo,Id,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Medico medico)
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
                    if (medico.MedicoPacientes == null)
                    {

                        List<MedicoPaciente> MedicoPacientes = new List<MedicoPaciente>();
                        medico.MedicoPacientes = MedicoPacientes;
                    }
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Buscar(string apellido)
        {
            var medicos = _context.Medicos
                .Where(x => x.Apellido.Contains(apellido));
            //ViewBag.Apellido = apellido;

            //var medicos = from m in _context.Medicos
            //              select m;

            //if (!String.IsNullOrEmpty(apellido))
            //{
            //    medicos = medicos.Where(m => m.NombreCompleto.Contains(apellido));
            //    ViewBag.Apellido = apellido;
            //}

            return View(medicos);
        }

        private int getUsuarioId()
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
            ViewData["MedicoId"] = getUsuarioId();

            return userIdValue;
        }


        public IActionResult ListarPacientes()
        {
            int id = getUsuarioId();
            var medico = _context.Medicos.Find(id);
            var pacientes = _context.MedicoPaciente
                        .Where(x => x.MedicoId == medico.Id)
                        .ToList();
            return View(pacientes);
            
        }

       
    }
}