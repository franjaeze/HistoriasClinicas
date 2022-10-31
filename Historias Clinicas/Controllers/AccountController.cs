﻿using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Historias_Clinicas.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly SignInManager<Persona> _signinManager;

        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager)
        {
            this._userManager = userManager;
            this._signinManager = signInManager;

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
                    await _signinManager.SignInAsync(pacienteACrear, isPersistent: false);
          
                    return RedirectToAction("Edit", "Personas", new { id = pacienteACrear.Id });

                }

                foreach (var error in resultadoCreacion.Errors)
                { 
                    ModelState.AddModelError(string.Empty, error.Description);

                }
            }

            return View(viewModel);
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login loginViewModel)
        {
            if (ModelState.IsValid)
            {         
                var resultado = await _signinManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.Recordarme, false);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }  
                ModelState.AddModelError(String.Empty, "Inicio de Sesión inválida");
            }
            return View(loginViewModel);
        }


        public async Task<IActionResult> CerrarSesion()
        {
             await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
