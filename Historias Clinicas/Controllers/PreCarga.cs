using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Historias_Clinicas.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasContext _context;

        private readonly List<string> roles = new List<string>() { Configs.AdminRolName , Configs.EmpleadoRolName, Configs.MedicoRolName, Configs.PacienteRolName, Configs.UsuarioRolName};
        
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
