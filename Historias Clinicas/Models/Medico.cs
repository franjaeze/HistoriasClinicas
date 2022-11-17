using Historias_Clinicas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {
        [Required(ErrorMessage = MensajeError.Requerido)]
        [Range(9999, 1000000, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression("([0-9]+)", ErrorMessage = MensajeError.NumerosPositivos)]

        [Display(Name = Alias.MatriculaNacional)]
        public int MatriculaNacional{ get; set; } 


        public Especialidad Especialidad { get; set; }  


        public List<MedicoPaciente> MedicoPacientes { get; set; }
    }
}
