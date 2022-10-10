using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class MedicoPaciente
    {
        

        [Key]
        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int MedicoId { get; set; }


        [Key]
        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int PacienteId { get; set; }


        public Medico Medico { get; internal set; }

        public Paciente Paciente { get; internal set; }

    }
}
