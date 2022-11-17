using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Historias_Clinicas.ViewModels
{
    public class RegistroUsuario
    {
        [Required(ErrorMessage = MensajeError.Requerido)]
        [EmailAddress(ErrorMessage = MensajeError.NoValido)]
        [Remote(action: "EmailDisponible", controller: "Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.PassConfirm)]
        [Compare("Password", ErrorMessage = MensajeError.PassMissMatch)]
        public string ConfirmacionPassword { get; set; }

    }
}
