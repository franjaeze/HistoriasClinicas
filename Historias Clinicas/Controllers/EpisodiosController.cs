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
using System.Security.Claims;

namespace Historias_Clinicas.Controllers
{
    [Authorize]

    public class EpisodiosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public EpisodiosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        // GET: Episodios
        public IActionResult Index()
        {
            var episodios = from m in _context.Episodios

                            orderby m.FechaYHoraInicio
                            select m;

            return View(episodios);

            ;
        }

        // GET: Episodios/Details/5
        public IActionResult Details(int? id, int paciente)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios
                .FirstOrDefault(m => m.Id == id);
            if (episodio == null)
            {
                return Content($"El episodio con id {id} no fue encontrado");
                // Se cambio del NotFound para que no se rompa todo
            }
            TempData["paciente"] = paciente;
            return View(episodio);
        }

        // GET: Episodios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Episodios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id,HistoriaClinicaId,EpicrisisId,Descripcion,Motivo,Internacion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,Especialidad")] Episodio episodio)
        {
            if (ModelState.IsValid)

            {
                episodio.EmpleadoId = getUsuarioId();

                var paciente = _context.Pacientes.Find(id);
                var historia = _context.HistoriasClinicas.Find(paciente.HistoriaClinicaId);
                episodio.HistoriaClinicaId = historia.Id;

                episodio.Id = 0;
                episodio.FechaYHoraInicio = DateTime.Now;
                episodio.EstadoAbierto = true;

                _context.Add(episodio);
                _context.SaveChanges();

                historia.Episodios.Add(episodio);
                _context.SaveChanges();


                List<Evolucion> Evoluciones = new List<Evolucion>();

                return RedirectToAction("Index", "Pacientes");
            }
            return View(episodio);
        }

        // GET: Episodios/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios.Find(id);
            if (episodio == null)
            {
                return NotFound();
            }
            return View(episodio);
        }

        // POST: Episodios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PacienteId,MedicoId,Descripcion,Motivo,Antecedentes,Internacion,FechaYHoraInicio,FechaYHoraAlta,FechaYHoraCierre,EstadoAbierto,EmpleadoId,Especialidad")] Episodio episodio)
        {
            if (id != episodio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(episodio);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EpisodioExists(episodio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Pacientes");
            }
            return View(episodio);
        }

        // GET: Episodios/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episodio = _context.Episodios
                .FirstOrDefault(m => m.Id == id);
            if (episodio == null)
            {
                return NotFound();
            }

            return View(episodio);
        }

        // POST: Episodios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var episodio = _context.Episodios.Find(id);
            _context.Episodios.Remove(episodio);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodioExists(int id)
        {
            return _context.Episodios.Any(e => e.Id == id);
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

        public IActionResult EvolucionesPorEpisodio(int id)
        {
            Episodio episodio = _context.Episodios.Find(id);

            var evoluciones = _context.Evoluciones
                .Where(x => x.EpisodioId == episodio.Id);

            ViewData["episodioId"] = id;

            return View(evoluciones);
        }

        public IActionResult CargarEvolucion(int id)
        {
            var episodio = _context.Episodios.Find(id);

            return RedirectToAction("Create", "Evoluciones", new { id = episodio.Id });
        }

        public IActionResult Cerrar(int id)
        {

            ViewBag.EpisodioId = id;

            var episodio = _context.Episodios.Find(id);

            if (episodio == null)
            {
                return NotFound();
            }

            if (EvolucionesAbiertas(id))
            {
                return RedirectToAction("NoPuedeCerrarse");
            }

             
            return RedirectToAction("Create", "Epicrisis", new { numero = id });
        }
        public IActionResult NoPuedeCerrarse(int i)
        {
            TempData["espisodioId"] = i;

            return View();
        }

        public bool EvolucionesAbiertas(int id)
        {
            bool EvolucionesAbiertasa = false;

            var episodio = _context.Episodios.Find(id);
            var evoluciones = _context.Evoluciones.Where(x => x.EpisodioId == episodio.Id);
            if (evoluciones.Any(x => x.EstadoAbierto) || !evoluciones.Any())
            {
                EvolucionesAbiertasa = true;
            }


            return EvolucionesAbiertasa;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CierreAdministrativo(int id, int paciente, [Bind("Id,EpicrisisId,Descripcion,Recomendacion,Especialidad")] Diagnostico diagnostico)
        {
            var episodioDb = _context.Episodios.Find(id);

            if (episodioDb == null)
            {
                return NotFound();
            }


            episodioDb.EstadoAbierto = false;
            episodioDb.FechaYHoraCierre = DateTime.Now;

            _context.Update(episodioDb);
            _context.SaveChanges();

            return View();
        }

        private bool epicrisisExiste(int i)
        {
            var existe = false;
            var epicrisis = _context.Epicrisis.Where(x => x.EpisodioId == i);

            if (epicrisis.Any())
            {
                existe = true;
            }

            return existe;
        }
        public IActionResult CierreAdministrativo(int id, int paciente)
        {
            if (epicrisisExiste(id))
            {

            }

            TempData["historiaId"] = paciente;
            TempData["EpisodioId"] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DarAlta(int id, [Bind("FechaYHoraAlta")] Episodio episodio)
        {


            var episodioDb = _context.Episodios.Find(id);

            if (episodioDb == null)
            {
                return NotFound();
            }


            episodioDb.EstadoAbierto = false;
            episodioDb.FechaYHoraCierre = DateTime.Now;
            episodioDb.FechaYHoraAlta = episodio.FechaYHoraAlta;

            //episodioDb.HistoriaClinicaId = episodio.HistoriaClinicaId;
            //episodioDb.EmpleadoId = episodio.EmpleadoId;
            //episodioDb.Descripcion = episodio.Descripcion;
            //episodioDb.Motivo = episodio.Motivo;
            //episodioDb.Especialidad = episodio.Especialidad;
            //episodioDb.FechaYHoraInicio = episodio.FechaYHoraInicio;
            //episodioDb.FechaYHoraAlta = episodio.FechaYHoraAlta;
            //episodioDb.Internacion = episodio.Internacion;


            _context.Update(episodioDb);
            _context.SaveChanges();


            //@TempData["historiaId"] = ;
            var idHistoria = episodioDb.HistoriaClinicaId;

            if (episodioDb.Descripcion == null)
            {
                return RedirectToAction("Create", "Diagnosticos");
            }
            return RedirectToAction("HistoriaClincaDePaciente", "HistoriaClinicas", new { id = idHistoria });
        }

        public IActionResult DarAlta(int id, int paciente)
        {
            TempData["historiaId"] = paciente;
            TempData["episodioId"] = id;
            TempData.Keep();
            return View();
        }

        public IActionResult EpisodiosAbiertos(int id)
        {
            var episodios = from e in _context.Episodios
                            select e;

            if (id !=0)
            {
                episodios = episodios.Where(m=> m.EstadoAbierto == true && m.HistoriaClinicaId == id);
            }

            var hca = _context.HistoriasClinicas.Find(id);
            int pacienteId = hca.PacienteId;
            TempData["PacienteId"] = pacienteId;


            return View(episodios);
        }



    }

}
