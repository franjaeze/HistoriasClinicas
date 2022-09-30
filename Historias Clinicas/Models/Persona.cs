using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [ Required (ErrorMessage = MensajeError.Requerido)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(50, MinimumLength =2, ErrorMessage = MensajeError.MinMaxString)]
        public String Apellido { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        public String Dni { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = MensajeError.MinMaxString)]
        public String Telefono { get; set; }


        public DateTime FechaDeAlta { get; set; }

        public Usuario Usuario { get; set; }
    }
}
