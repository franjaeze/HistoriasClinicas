using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text;
using Historias_Clinicas.Models;
using Historias_Clinicas.Helpers;

namespace Historias_Clinicas.Models
{
  
    public class Persona
    {  

        public int Id { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        public String Nombre { get; set; }


        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        public String SegundoNombre { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        [DataType(DataType.Text)]
        public String Apellido { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(Alias.DNI)]
        public String Dni { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15,MinimumLength =7, ErrorMessage = MensajeError.MinMaxString)]
        public String Telefono { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaDeAlta { get; set; }


        public Usuario Usuario { get; set; }

        [DataType(Alias.NombreCompleto)]
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
