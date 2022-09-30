using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {

        [Required(ErrorMessage = MensajeError.Requerido)]
        [Required(ErrorMessage = MensajeError.UnaOpcion)] //Debe haber al menos 1 Matricula. Pueden existir MN y MP
        public List<TipoDeMatricula> TiposDeMatriculas { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(7, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int Matricula{ get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [Required(ErrorMessage = MensajeError.UnaOpcion)]//Al menos 1 Especialidad
        public List<Especialidad> Especialidades { get; set; }

    }
}
