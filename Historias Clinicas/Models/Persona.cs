using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text;
using Historias_Clinicas.Models;
using Historias_Clinicas.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas.Models
{

    public class Persona : IdentityUser<int>
    {

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = MensajeError.SoloLetras)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        public String Nombre { get; set; }


        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = MensajeError.SoloLetras)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        [Display(Name = Alias.SegundoNombre)]
        public String SegundoNombre { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = MensajeError.SoloLetras)]
        [DataType(DataType.Text)]
        public String Apellido { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [RegularExpression("([0-9]+)", ErrorMessage = MensajeError.SoloNumeros)]
        [Display(Name = Alias.DNI)]
        public int Dni { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [EmailAddress]
        [Display(Name = Alias.Email)]
        public override String Email {
            get { return base.Email; }
            set { base.Email = value; }
        }

      


        [Required(ErrorMessage = MensajeError.Requerido)]
        [Phone]
        [StringLength(15, MinimumLength = 7, ErrorMessage = MensajeError.MinMaxString)]
        public String Telefono { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = Alias.FechaDeAlta)]
        public DateTime FechaDeAlta { get; set; }


        public Direccion Direccion { get; set; }
   

        [Display(Name = Alias.NombreCompleto)]
        public String NombreCompleto
        {
            get
            {
                if (string.IsNullOrEmpty(Apellido) && string.IsNullOrEmpty(Nombre)) return "Sin Definir";
                if (string.IsNullOrEmpty(Apellido)) return Nombre;
                if (string.IsNullOrEmpty(Nombre)) return Apellido.ToUpper();

                return $"{Apellido.ToUpper()}, {Nombre}";
            }
        }


    }
}
