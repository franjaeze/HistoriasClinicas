using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Historias_Clinicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Historias_Clinicas.Data;
using Historias_Clinicas.ViewModels;

namespace Historias_Clinicas.Controllers
{


    public class AccountController : Controller
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signinManager;
        private readonly RoleManager<Rol> _rolManager;
        private const string passDefault = "Password1!";

        public AccountController(
            HistoriasClinicasContext context,
            UserManager<Persona> userManager,
            SignInManager<Persona> signinManager,
            RoleManager<Rol> rolManager
            )
        {
            this._context = context;
            this._userManager = userManager;
            this._signinManager = signinManager;
            this._rolManager = rolManager;
        }

        [HttpGet]
        public IActionResult EmailDisponible(string email)
        {
            var emailUsado = _context.Personas.Any(p => p.Email == email);

            if (!emailUsado)
            {

                return Json(true);
            }
            else
            {

                return Json($"El correo {email} ya está en uso.");
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Registrar(RegistroUsuario viewModel)
        {


            if (ModelState.IsValid)
            {
                Paciente pacienteACrear = new Paciente();
                pacienteACrear.Email = viewModel.NombreUsuario;
                pacienteACrear.UserName = viewModel.NombreUsuario;

                var resultadoCreacion = await _userManager.CreateAsync(pacienteACrear, viewModel.Password);

                if (resultadoCreacion.Succeeded)
                {

                    await CrearRolesBase();


                    var resultado = await _userManager.AddToRoleAsync(pacienteACrear, "Paciente");

                    if (resultado.Succeeded)
                    {

                        await _signinManager.SignInAsync(pacienteACrear, isPersistent: false);
                        return RedirectToAction("Edit", "Paciente", new { id = pacienteACrear.Id });
                    }


                }


                foreach (var error in resultadoCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }

        public ActionResult IniciarSesion(string returnurl)
        {
            TempData["returnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Login viewModel)
        {
            string returnUrl = TempData["returnUrl"] as string;

            if (ModelState.IsValid)
            {
                var resultadoSignIn = await _signinManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.Recordarme, false);

                if (resultadoSignIn.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Inicio de sesión inválido");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> CerrarSesion()
        {
            await _signinManager.SignOutAsync();

            return RedirectToAction("Index", "home");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }


        private async Task CrearRolesBase()
        {
            List<string> roles = new List<string>() { "Administrator", "Paciente", "Empleado", "UsuarioBase", "Medico" };

            foreach (string rol in roles)
            {
                await CrearRole(rol);
            }
        }

        private async Task CrearRole(string rolName)
        {
            if (!await _rolManager.RoleExistsAsync(rolName))
            {
                await _rolManager.CreateAsync(new Rol(rolName));
            }
        }


        public async Task<IActionResult> CrearAdmin()
        {
            Persona account = new Persona()
            {
                Nombre = "Administrador",
                Apellido = "Dios",
                Email = "administrador@administrador.com",
                UserName = "administrador@administrador.com"
            };

            var resuAdm = await _userManager.CreateAsync(account, passDefault);
            if (resuAdm.Succeeded)
            {
                string rolAdm = "Administrador";
                await CrearRole(rolAdm);
                await _userManager.AddToRoleAsync(account, rolAdm);
                TempData["Mensaje"] = $"Empleado creado {account.Email} y {passDefault}";
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CrearEmpleado()
        {
            Empleado account = new Empleado()
            {
                Nombre = "Empleado",
                Apellido = "Del Año",
                Email = "empleado@empleado.com",
                UserName = "empleado@empleado.com"
            };

            var resuAdm = await _userManager.CreateAsync(account, passDefault);
            if (resuAdm.Succeeded)
            {
                string rolAdm = "Empleado";
                await CrearRole(rolAdm);
                await _userManager.AddToRoleAsync(account, rolAdm);
                TempData["Mensaje"] = $"Empleado creado {account.Email} y {passDefault}";
            }

            return RedirectToAction("Index", "Home");
        }
    }
    //public class AccountController : Controller
    //{

//    private readonly UserManager<Persona> _usermanager;
//    public AccountController(UserManager<Persona> usermanager)
//    {
//        this._usermanager = usermanager;
//    }

//    public IActionResult Registrar()
//    {
//        return View();
//    }
//}
