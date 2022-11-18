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

            if (ModelState.IsValid)
            {
                paciente.FechaDeAlta = DateTime.Today;
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

                    _context.Update(paciente);
                    _context.SaveChanges();

                }

                return RedirectToAction(nameof(Index));
            }
            return View(paciente);

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
                    pacienteEnDb.FechaDeAlta = paciente.FechaDeAlta;


                    if (paciente.HistoriaClinicaId == null)
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

        private IActionResult HistoriaClinicaPaciente()
        {
            int userId = getUsuarioId();
            var historia = _context.HistoriasClinicas
                            .Find(userId);
            var episodios = _context.Episodios
                            .Where(s => s.HistoriaClinicaId == historia.Id);

            return View(episodios);
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

            return userIdValue;
        }

        private IActionResult listarEpisodios(int id)
        {
            var paciente = _context.Pacientes.Find(id);
            var historia = _context.HistoriasClinicas
                            .Find(paciente);
            var episodios = _context.Episodios
                            .Where(s => s.HistoriaClinicaId == historia.Id);

            return View(episodios);
        }
    }
}
