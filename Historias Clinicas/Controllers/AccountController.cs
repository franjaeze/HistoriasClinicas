using Historias_Clinicas.Data;
using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;
using Historias_Clinicas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signinManager;
        private readonly RoleManager<Rol> _roleManager;


        public AccountController(UserManager<Persona> userManager,
            SignInManager<Persona> signInManager,
            HistoriasClinicasContext contexto,
            RoleManager<Rol> roleManager)
        {
            this._userManager = userManager;
            this._signinManager = signInManager;
            this._roleManager = roleManager;
        }

        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]


        public async Task<IActionResult> Registrar([Bind("Email,Password,ConfirmacionPassword")] RegistroUsuario viewModel)
        {

            if (ModelState.IsValid)
            {
                Paciente pacienteACrear = new Paciente();
                {
                    pacienteACrear.Email = viewModel.Email;
                    pacienteACrear.UserName = viewModel.Email;
            
                }
                var resultadoCreacion = await _userManager.CreateAsync(pacienteACrear, viewModel.Password);

                if (resultadoCreacion.Succeeded)
                {
                    var resultadoAddRole = await _userManager.AddToRoleAsync(pacienteACrear, Configs.PacienteRolName);

                    if (resultadoAddRole.Succeeded)
                    {
                        await _signinManager.SignInAsync(pacienteACrear, isPersistent: false);

                        return RedirectToAction("CompletarRegistro", "Pacientes", new { id = pacienteACrear.Id });
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, $" No se pudo agregar el rol de {Configs.PacienteRolName}");
                    }
                }

                foreach (var error in resultadoCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }
            }

            return View(viewModel);
        }

        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login viewModel)
        {
            string returnUrl = TempData["ReturnUrl"] as string;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                var resultado = await _signinManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.Recordarme, false);
                if (resultado.Succeeded)
                {
                   
                    if (User.IsInRole("Paciente"))
                    {
                        return RedirectToAction("MenuPaciente", "Pacientes");
                    }
                    if (User.IsInRole("Medico"))
                    {
                        return RedirectToAction("MenuMedico", "Medicos");
                    } if (User.IsInRole("Empleado"))
                     {
                        return RedirectToAction("MenuEmpleado", "Empleados");

                    }

                }
                ModelState.AddModelError(String.Empty, "Inicio de Sesión inválida");
            }
            return View(viewModel);
        }


        public async Task<IActionResult> CerrarSesion()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "EsEmpleado")]
        public async Task<IActionResult> ListarRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult AccesoDenegado(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailDisponible(string email)
        {
            var PersonaExistente = await _userManager.FindByEmailAsync(email);
            if (PersonaExistente == null)
            {
                return Json(true);
             }
            else
            {
                return Json($"El correo {email} ya esta en uso.");
             }
           }

    }

    }




