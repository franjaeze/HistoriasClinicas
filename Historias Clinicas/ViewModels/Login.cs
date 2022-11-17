using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Historias_Clinicas.ViewModels
{
    public class Login
    {

        [Required(ErrorMessage = MensajeError.Requerido)]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = MensajeError.NoValido)]
        public string Email { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }


        public bool Recordarme { get; set; } = false;
    }
}
