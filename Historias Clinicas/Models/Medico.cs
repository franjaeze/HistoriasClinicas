using Historias_Clinicas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {

        public int Id { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(8, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        //[RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        [Display(Name = Alias.MatriculaNacional)]
        public int MatriculaNacional{ get; set; }

        public Especialidad Especialidad { get; set; }

        [Display(Name = Alias.Activo)]
        public Boolean EstaActivo { get; set; }

        public List<MedicoPaciente> MedicoPacientes { get; set; }
    }
}
