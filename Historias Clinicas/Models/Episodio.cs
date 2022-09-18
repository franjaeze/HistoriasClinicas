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
        public int MedicoId { get; set; }

        public string Descripcion { get; set; }

        public DateAndTime FechaYHoraInicio { get; set; }

        public DateAndTime FechaYHoraAlta { get; set; }

        public DateAndTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get; set; }

        public int EmpleadoId { get; set; }

        public List<Evolucion> Evoluciones { get; set; }

        public Epicrisis Epicrisis { get; set; }

    }
}
