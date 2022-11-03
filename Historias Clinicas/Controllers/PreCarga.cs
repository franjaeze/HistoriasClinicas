using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasContext _context;

        private readonly List<string> roles = new List<string>() { "Usuario", "Admin", "Paciente", "Empleado", "Medico" };
        
        public PreCarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public IActionResult Seed() // Semilla(Origen) de la base de datos
        {
            CrearRoles().Wait();
            CrearAdmin().Wait();
            CrearPacientes().Wait();
            CrearEmpleados().Wait();
            CrearMedicos().Wait();

            return RedirectToAction("Index","Home", new { mensaje = "Precarga finalizada"});
        }

        private async Task CrearMedicos()
        {
            if (!_context.Medicos.Any())
            {

                Medico medico = new Medico()
                {
                    Nombre = "Charly",
                    Apellido = "Garcia",
                    Dni = 55667788,
                    Email = "charly@ort.edu.ar"

                };
                _context.Personas.Add(persona);
                _context.SaveChanges();

                Persona persona2 = new Persona()
                {

                    Nombre = "Luis",
                    Apellido = "Alberto Spinetta",
                    Dni = 55228788,
                    Email = "LASy@ort.edu.ar"

                };
                _context.Personas.Add(persona2);
                _context.SaveChanges();
            }
        }
    }

        private async Task CrearEmpleados()
        {

        }

        private async Task CrearPacientes()
        {

        }

        private async Task CrearAdmin()
        {

        }

        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }
    }
}
