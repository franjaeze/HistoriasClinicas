using Historias_Clinicas.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Controllers
{
    public class MedicosController : Controller
    {
        private readonly HistoriasClinicasContext _context;

        public MedicosController(HistoriasClinicasContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
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
