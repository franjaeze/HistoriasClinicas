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
        public int MedicoId { get; set; }


        [Key]
        [Required(ErrorMessage = MensajeError.Requerido)]
        public int PacienteId { get; set; }


        public Medico Medico { get; internal set; }

        public Paciente Paciente { get; internal set; }

    }
}
