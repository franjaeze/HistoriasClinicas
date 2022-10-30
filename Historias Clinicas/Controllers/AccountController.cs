using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Historias_Clinicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<Persona> _usermanager;
        public AccountController(UserManager<Persona> usermanager)
        {
            this._usermanager = usermanager;
        }

        public IActionResult Registrar()
        {
            return View();
        }
    }
}
