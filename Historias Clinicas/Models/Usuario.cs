using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class Usuario : Persona
    {

    
        public DateTime FechaAlta { get; set; }

        
       [Required(ErrorMessage = MensajeError.Requerido)]
       [StringLength(20,MinimumLength =6, ErrorMessage = MensajeError.MinMaxString)]
        public String Password { get; set; }
    }
}
