using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class MedicoPaciente
    {
        [Required]
        [Key]
        public int MedicoId { get; set; }

        [Required]
        [Key]
        public int PacienteId { get; set; }

    }
}
