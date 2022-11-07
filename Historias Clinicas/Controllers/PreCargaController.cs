﻿using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class PreCargaController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistoriasClinicasContext _context;

        private readonly List<string> roles = new List<string>() { "Usuario", "Admin", "Paciente", "Empleado", "Medico" };
        
        public PreCargaController(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public IActionResult Seed() // Semilla(Origen) de la base de datos
        {
            CrearRoles().Wait();
            //CrearAdmin().Wait();
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
                    Nombre = "Mateo",
                    SegundoNombre = "Agustin",
                    Apellido = "Bellomo",
                    Dni = 42375111,
                    Email = "mateo@ort.edu.ar",
                    Telefono = "1158889987",
                    FechaDeAlta = new DateTime(2015, 12, 25),
                    MatriculaNacional = 10000,
                    Especialidad = Especialidad.ClinicaMedica
                };
                _context.Medicos.Add(medico);
                _context.SaveChanges();

                Medico medico2 = new Medico()
                {

                    Nombre = "Camila",
                    SegundoNombre = "Belen",
                    Apellido = "Szesko",
                    Dni = 42375112,
                    Email = "camila@ort.edu.ar",
                    Telefono = "1159999988",
                    FechaDeAlta = new DateTime(2015, 12, 28),
                    MatriculaNacional = 10001,
                    Especialidad = Especialidad.Cardiologia

                };
                _context.Medicos.Add(medico2);
                _context.SaveChanges();
            }
        }
    

        private async Task CrearEmpleados()
        {
        if (!_context.Empleados.Any())
        {

            Empleado empleado = new Empleado()
            {
                Nombre = "Francisco",
                SegundoNombre = "Javier",
                Apellido = "Veron",
                Dni = 42375222,
                Email = "francisco@ort.edu.ar",
                Telefono = "1166669987",
                FechaDeAlta = new DateTime(2018, 12, 25),
                Legajo = 1
            };
            _context.Empleados.Add(empleado);
            _context.SaveChanges();

            Empleado empleado2 = new Empleado()
            {
                Nombre = "Paola",
                SegundoNombre = "Yanina",
                Apellido = "Quinionez",
                Dni = 42300222,
                Email = "paola@ort.edu.ar",
                Telefono = "1177779987",
                FechaDeAlta = new DateTime(2014, 12, 10),
                Legajo = 2

            };
            _context.Empleados.Add(empleado2);
            _context.SaveChanges();
        }
    }

        private async Task CrearPacientes()
        {
        if (!_context.Pacientes.Any())
        {

            Paciente paciente = new Paciente()
            {
                Nombre = "Valentino",
                SegundoNombre = "Pepe",
                Apellido = "Caseres",
                Dni = 41115222,
                Email = "valentino@ort.edu.ar",
                Telefono = "1100069987",
                FechaDeAlta = new DateTime(2012, 12, 14),
                ObraSocialP = Cobertura.OSDE,
                HistoriaClincaId = 1
            };
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            Paciente paciente2 = new Paciente()
            {
                Nombre = "Juan",
                SegundoNombre = "Jose",
                Apellido = "Cruz",
                Dni = 42321222,
                Email = "juan@ort.edu.ar",
                Telefono = "1177047980",
                FechaDeAlta = new DateTime(2011, 12, 05),
                ObraSocialP = Cobertura.OSECAC,
                HistoriaClincaId = 2

            };
            _context.Pacientes.Add(paciente2);
            _context.SaveChanges();
        }
    }

        //private async Task CrearAdmin()
        //{

        //}

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