using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasContext hcontext;

        public MedicosController(HistoriasClinicasContext context)
        {
            this.hcontext = context;
        }

        public IActionResult Index()
        {
            var medicos = hcontext.Medicos.ToList();

            return View();
        }
        //Ofrece el formulario de creacion
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //para crear un medico
        [HttpPost]
        public IActionResult Create(int id, String nombre, string segundoNombre, String apellido, String dni, String email, String telefono, DateTime fechaAlta, Usuario usuario, int matriculan, int matriculap, List<Especialidad> especialidades, Boolean activo)
        {
            Medico medico = new Medico() { Id = id, Nombre = nombre, SegundoNombre = segundoNombre, Apellido = apellido, Dni = dni, Email = email, Telefono = telefono, FechaDeAlta = fechaAlta, Usuario = usuario, MatriculaNacional = matriculan, MatriculaProvincial = matriculap, Especialidades = especialidades, EstaActivo = activo };
            hcontext.Medicos.Add(medico);
            hcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
