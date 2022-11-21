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
    public class PacientesController : Controller
    {

        private readonly HistoriasClinicasContext _context;

        public PacientesController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public  IActionResult Index()
        {
            //PacientesCollections();
            return View( _context.Pacientes.ToList());
        }

        public IActionResult MenuPaciente()
        {
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        // GET: Pacientes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = _context.Pacientes
                .FirstOrDefault(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ObraSocial,Nombre,SegundoNombre,Apellido,Dni,Email,Telefono,FechaDeAlta")] Paciente paciente)
        {

            VerificarDni(paciente);

            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                _context.SaveChanges();

                if (paciente.HistoriaClinicaId == null)
                {
                    HistoriaClinica historiaClinica = new HistoriaClinica()
                    {
                        PacienteId = paciente.Id,
                        Episodios = new List<Episodio>()
                    };


                    _context.Add(historiaClinica);
                    _context.SaveChanges();

                    paciente.HistoriaClinicaId = historiaClinica.Id;

                    List<MedicoPaciente> MedicosPaciente = new List<MedicoPaciente>();
                    paciente.MedicosPaciente = MedicosPaciente;

                    _context.Update(paciente);
                    _context.SaveChanges();

                }

                return RedirectToAction(nameof(Index));
            }
            return View(paciente);

           }

        private bool DniExist(Paciente paciente)
        {
            bool devolver = false;
            if (paciente.Dni != 0)
            {
                if (paciente.Id != 0)
                {
                    devolver = _context.Personas.Any(p => p.Dni == paciente.Dni && p.Id != paciente.Id);
                }
                else
                {
                    devolver = _context.Personas.Any(p => p.Dni == paciente.Dni);
                }
            }
            return devolver;
        }

        private void VerificarDni(Paciente paciente)
        {
            if (DniExist(paciente))
            {
                ModelState.AddModelError("Dni", "Ya existe un persona con el dni ingresado");
            }
        }

        // GET: Pacientes/Edit/5

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente =  _context.Pacientes.Find(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public IActionResult Edit(int id,[Bind("Id,ObraSocial,Nombre,SegundoNombre,Apellido,HistoriaClinicaId,Dni,Email,Telefono,FechaDeAlta")] Paciente paciente)
        {

            if (id != paciente.Id)
            {

                return NotFound();
            }

            VerificarDni(paciente);

            if (ModelState.IsValid)
            {
                try
                {
                    var pacienteEnDb = _context.Pacientes.Find(paciente.Id);

                    if (pacienteEnDb == null)
                    {
                        return NotFound();

                    }

                    pacienteEnDb.Dni = paciente.Dni;
                    pacienteEnDb.Telefono = paciente.Telefono;
                    pacienteEnDb.ObraSocial = paciente.ObraSocial;
                    pacienteEnDb.HistoriaClinicaId = paciente.HistoriaClinicaId;
                    pacienteEnDb.Nombre = paciente.Nombre;
                    pacienteEnDb.SegundoNombre = paciente.SegundoNombre;
                    pacienteEnDb.Apellido = paciente.Apellido;
                    pacienteEnDb.Email = paciente.Email;

                    var fechaDefault = new DateTime(0001, 1, 1, 00, 00, 00);

                    if (pacienteEnDb.FechaDeAlta == fechaDefault)
                    {
                        pacienteEnDb.FechaDeAlta = DateTime.Today;
                    }

                    if (pacienteEnDb.MedicosPaciente == null)
                    {
                        List<MedicoPaciente> MedicosPaciente = new List<MedicoPaciente>();
                        pacienteEnDb.MedicosPaciente = MedicosPaciente;
                    }

                    _context.SaveChanges();

                    if (pacienteEnDb.HistoriaClinicaId == null)
                    {
                        HistoriaClinica historiaClinica = new HistoriaClinica()
                        {
                            PacienteId = paciente.Id,
                            Episodios = new List<Episodio>()
                        };

                        _context.Add(historiaClinica);
                        _context.SaveChanges();

                        pacienteEnDb.HistoriaClinicaId = historiaClinica.Id;

                       
                        _context.SaveChanges();
                    }
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             
                return RedirectToAction(nameof(MenuPaciente));
            }
            return View(paciente);
        }

        // GET: Pacientes/Delete/5

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = _context.Pacientes
                .FirstOrDefault(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }

        public IActionResult MiHistoriaClinica()
        {
            int Id = getUsuarioId();
          
            return RedirectToAction("HistoriaClinicaDePaciente", "HistoriaClinicas", new { id = Id } );
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

            //ViewData["PacienteId"] = getUsuarioId();
            return userIdValue;
        }

        public IActionResult SacarTurno(int id)
        {
            var paciente = _context.Pacientes.Find(getUsuarioId());
            var medico = _context.Medicos.Find(id);
            MedicoPaciente MedicoPaciente = new MedicoPaciente()
            {
                MedicoId = id,
                PacienteId = getUsuarioId(),
                Medico = medico,
                Paciente = paciente
            };

            _context.MedicoPaciente.Add(MedicoPaciente);
            //medico.MedicoPacientes.Add(MedicoPaciente);
            //paciente.MedicosPaciente.Add(MedicoPaciente);
            _context.SaveChanges();

            return View();
        }

        public IActionResult ListarMedicos(int? id)
        {
            var paciente = _context.Pacientes.Find(id);
            var medicos = _context.MedicoPaciente.Find(paciente.Id);
            return View(medicos);

        }

    }
}