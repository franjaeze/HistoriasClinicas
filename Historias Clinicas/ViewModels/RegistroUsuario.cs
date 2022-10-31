using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.ViewModels
{
    public class RegistroUsuario
    {
        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MensajeError.MinMaxString)]
        [Display(Name = Alias.NombreUsuario)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MensajeError.MinMaxString)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.ContraseniaUsuario)]
        public String Password { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = MensajeError.MinMaxString)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.ConfirmarContrasenia)]
        [Compare("Password", ErrorMessage = MensajeError.ContraseniaDiferente)]
        public String ConfirmacionPassword { get; set; }
    }
}
