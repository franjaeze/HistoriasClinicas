using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Episodio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int MedicoId { get; set; }

        public string Descripcion { get; set; }

        public String Motivo { get; set; }

        public String Antecedentes { get; set; }

        public Boolean Internacion { get; set; }

        public DateAndTime FechaYHoraInicio { get; set; }

        public DateAndTime FechaYHoraAlta { get; set; }

        public DateAndTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        public List<Evolucion> Evoluciones { get; set; }

        public Epicrisis Epicrisis { get; set; }

        public Especialidad Especialidad { get; set; }

    }
}
