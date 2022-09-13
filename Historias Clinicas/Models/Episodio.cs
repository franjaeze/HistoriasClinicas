using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Historias_Clinicas.Models
{
    public class Episodio
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }

        public int MedicoId { get; set; }

        public string Descripcion { get; set; }

        public String Motivo { get; set; }

        public String Antecedentes { get; set; }

        public Boolean Internacion { get; set; }

        public DateAndTime FechaYHoraInicio { get; set; }

        public DateAndTime FechaYHoraAlta { get; set; }

        public DateAndTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get; set; }

        public int EmpleadoId { get; set; }

        public List<Evolucion> Evoluciones { get; set; }

        public Epicrisis Epicrisis { get; set; }

        public Especialidad Especialidad { get; set; }

    }
}
