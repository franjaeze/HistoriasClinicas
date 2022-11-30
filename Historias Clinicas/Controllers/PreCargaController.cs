using Historias_Clinicas.Data;
using Historias_Clinicas.Helpers;
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

        private readonly List<string> roles = new List<string>() { Configs.MedicoRolName, Configs.PacienteRolName, Configs.EmpleadoRolName };

        public PreCargaController(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistoriasClinicasContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public IActionResult Seed() // Semilla(Origen) de la base de datos
        {
            CrearRoles().Wait();
            CrearPacientes().Wait();
            CrearEmpleados().Wait();
            CrearMedicos().Wait();

            return RedirectToAction("Index", "Home", new { mensaje = "Precarga finalizada" });
        }

        private async Task CrearMedicos()
        {
            if (!_context.Medicos.Any())
            {

                Medico Medico1 = new Medico()
                {
                    Nombre = "Mateo",
                    SegundoNombre = "Agustin",
                    Apellido = "Bellomo",
                    Dni = 42375111,
                    Email = "mateo@ort.edu.ar",
                    UserName = "mateo@ort.edu.ar",
                    Telefono = "1158889987",
                    FechaDeAlta = new DateTime(2015, 12, 25),
                    MatriculaNacional = 10000,
                    Especialidad = Especialidad.ClinicaMedica
                };

                await _userManager.CreateAsync(Medico1, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Medico1, Configs.MedicoRolName);

                Direccion Direccion1 = new Direccion()
                {
                    PersonaId = Medico1.Id,
                    Calle = "Lavalle",
                    Altura = "100",
                    Piso = "1",
                    Departamento = "A",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion1);
                await _context.SaveChangesAsync();

                Medico Medico2 = new Medico()
                {

                    Nombre = "Camila",
                    SegundoNombre = "Belen",
                    Apellido = "Szesko",
                    Dni = 42375112,
                    Email = "camila@ort.edu.ar",
                    UserName = "camila@ort.edu.ar",
                    Telefono = "1159999988",
                    FechaDeAlta = new DateTime(2015, 12, 28),
                    MatriculaNacional = 10001,
                    Especialidad = Especialidad.Cardiologia

                };

                await _userManager.CreateAsync(Medico2, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Medico2, Configs.MedicoRolName);

                Direccion Direccion2 = new Direccion()
                {
                    PersonaId = Medico2.Id,
                    Calle = "Laprida",
                    Altura = "200",
                    Piso = "2",
                    Departamento = "B",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion2);
                await _context.SaveChangesAsync();



                var paciente1 = _context.Pacientes.Find(1);

                MedicoPaciente MedicoPaciente1 = new MedicoPaciente()
                {
                    MedicoId = Medico1.Id,
                    PacienteId = paciente1.Id,
                    Medico = Medico1,
                    Paciente = paciente1
                };

                _context.MedicoPaciente.Add(MedicoPaciente1);
                await _context.SaveChangesAsync();


                var paciente2 = _context.Pacientes.Find(2);

                MedicoPaciente MedicoPaciente2 = new MedicoPaciente()
                {
                    MedicoId = Medico2.Id,
                    PacienteId = paciente2.Id,
                    Medico = Medico2,
                    Paciente = paciente2
                };

                _context.MedicoPaciente.Add(MedicoPaciente2);
                await _context.SaveChangesAsync();
            }
        }


        private async Task CrearEmpleados()
        {
            if (!_context.Empleados.Any())
            {

                Empleado Empleado1 = new Empleado()
                {
                    Nombre = "Francisco",
                    SegundoNombre = "Javier",
                    Apellido = "Veron",
                    Dni = 42375222,
                    Email = "francisco@ort.edu.ar",
                    UserName = "francisco@ort.edu.ar",
                    Telefono = "1166669987",
                    FechaDeAlta = new DateTime(2018, 12, 25),
                    Legajo = 1
                };

                await _userManager.CreateAsync(Empleado1, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Empleado1, Configs.EmpleadoRolName);

                Direccion Direccion3 = new Direccion()
                {
                    PersonaId = Empleado1.Id,
                    Calle = "Roca",
                    Altura = "300",
                    Piso = "3",
                    Departamento = "C",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion3);
                await _context.SaveChangesAsync();

                Empleado Empleado2 = new Empleado()
                {
                    Nombre = "Paola",
                    SegundoNombre = "Yanina",
                    Apellido = "Quinionez",
                    Dni = 42300222,
                    Email = "paola@ort.edu.ar",
                    UserName = "paola@ort.edu.ar",
                    Telefono = "1177779987",
                    FechaDeAlta = new DateTime(2014, 12, 10),
                    Legajo = 2

                };

                await _userManager.CreateAsync(Empleado2, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Empleado2, Configs.EmpleadoRolName);

                Direccion Direccion4 = new Direccion()
                {
                    PersonaId = Empleado2.Id,
                    Calle = "San Martin",
                    Altura = "400",
                    Piso = "4",
                    Departamento = "D",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion4);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CrearPacientes()
        {
            if (!_context.Pacientes.Any())
            {
                Paciente Paciente1 = new Paciente()
                {
                    Nombre = "Valentino",
                    SegundoNombre = "Pepe",
                    Apellido = "Caseres",
                    Dni = 41115222,
                    Email = "valentino@ort.edu.ar",
                    UserName = "valentino@ort.edu.ar",
                    Telefono = "1100069987",
                    FechaDeAlta = new DateTime(2012, 12, 14),
                    ObraSocial = Cobertura.OSDE,
                    HistoriaClinicaId = 1
                };
                await _userManager.CreateAsync(Paciente1, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Paciente1, Configs.PacienteRolName);

                Direccion Direccion5 = new Direccion()
                {
                    PersonaId = Paciente1.Id,
                    Calle = "Irigoyen",
                    Altura = "3000",
                    Piso = "5",
                    Departamento = "D",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion5);
                await _context.SaveChangesAsync();

                HistoriaClinica HistoriaClinica1 = new HistoriaClinica()
                {
                    PacienteId = Paciente1.Id,
                    Episodios = new List<Episodio>()
                };

                _context.HistoriasClinicas.Add(HistoriaClinica1);
                await _context.SaveChangesAsync();

                Episodio Episodio1 = new Episodio()
                {
                    Descripcion = "El paciente se encontraba andando en skate",
                    Motivo = "Traumatismo en pierna izquierda",
                    FechaYHoraInicio = DateTime.Today,
                    FechaYHoraAlta = DateTime.Today,
                    FechaYHoraCierre = DateTime.Today,
                    EstadoAbierto = true,
                    Especialidad = Especialidad.Enfermeria,
                    HistoriaClinicaId = HistoriaClinica1.Id,
                    EmpleadoId = 3
                };

                _context.Episodios.Add(Episodio1);
                await _context.SaveChangesAsync();


                Evolucion Evolucion1 = new Evolucion()
                {
                    EpisodioId = Episodio1.Id,
                    MedicoId = 5,
                    EstadoAbierto = true,
                    DescripcionAtencion = "Continuar con Kinesiologia",
                    Notas = new List<Nota>(),
                    FechaYHoraInicio = DateTime.Today,
                    FechaYHoraAlta = DateTime.Today,
                    FechaYHoraCierre = DateTime.Today
                };

                _context.Evoluciones.Add(Evolucion1);
                await _context.SaveChangesAsync();

                Nota Nota1 = new Nota()
                {
                    EvolucionId = Evolucion1.Id,
                    Mensaje = "Subir dosis pastillas",
                    FechaYHora = DateTime.Today,
                    EmpleadoId = 3
                };

                _context.Notas.Add(Nota1);
                await _context.SaveChangesAsync();



                Paciente Paciente2 = new Paciente()
                {
                    Nombre = "Juan",
                    SegundoNombre = "Jose",
                    Apellido = "Cruz",
                    Dni = 42321222,
                    Email = "juan@ort.edu.ar",
                    UserName = "juan@ort.edu.ar",
                    Telefono = "1177047980",
                    FechaDeAlta = new DateTime(2011, 12, 05),
                    ObraSocial = Cobertura.OSECAC,
                    HistoriaClinicaId = 2
                };
                await _userManager.CreateAsync(Paciente2, Configs.PasswordGenerica);
                await _userManager.AddToRoleAsync(Paciente2, Configs.PacienteRolName);


                Direccion Direccion6 = new Direccion()
                {
                    PersonaId = Paciente2.Id,
                    Calle = "Malaver",
                    Altura = "4000",
                    Piso = "6",
                    Departamento = "E",
                    Localidad = "Vicente Lopez"
                };

                _context.Direcciones.Add(Direccion6);
                await _context.SaveChangesAsync();


                HistoriaClinica HistoriaClinica2 = new HistoriaClinica()
                {
                    PacienteId = Paciente2.Id,
                    Episodios = new List<Episodio>()
                };

                _context.HistoriasClinicas.Add(HistoriaClinica2);
                await _context.SaveChangesAsync();

                Episodio Episodio2 = new Episodio()
                {
                    Descripcion = "El paciente se encontraba accidentado con fuego",
                    Motivo = "Quemaduras",
                    FechaYHoraInicio = DateTime.Today,
                    FechaYHoraAlta = DateTime.Today,
                    FechaYHoraCierre = DateTime.Today,
                    EstadoAbierto = true,
                    Especialidad = Especialidad.Dermatologia,
                    HistoriaClinicaId = HistoriaClinica2.Id,
                    EmpleadoId = 4
                };

                _context.Episodios.Add(Episodio2);
                await _context.SaveChangesAsync();

                Evolucion Evolucion2 = new Evolucion()
                {
                    EpisodioId = Episodio2.Id,
                    MedicoId = 6,
                    EstadoAbierto = true,
                    DescripcionAtencion = "Las marcas estan achicandose",
                    Notas = new List<Nota>(),
                    FechaYHoraInicio = DateTime.Today,
                    FechaYHoraAlta = DateTime.Today,
                    FechaYHoraCierre = DateTime.Today
                };

                _context.Evoluciones.Add(Evolucion2);
                await _context.SaveChangesAsync();


                Nota Nota2 = new Nota()
                {
                    EvolucionId = Evolucion2.Id,
                    Mensaje = "Probar otra marca crema",
                    FechaYHora = DateTime.Today,
                    EmpleadoId = 4
                };

                _context.Notas.Add(Nota2);
                await _context.SaveChangesAsync();
            }
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
