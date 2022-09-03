using Microsoft.VisualBasic;
using System;

namespace Historias_Clinicas.Models
{
    public class Episodio
    {

        public string Motivo { get; set; }

        public string Descripcion { get; set; }

        public DateAndTime FechaYHoraInicio { get; set; }

        public DateAndTime FechaYHoraAlta { get; set; }

        public DateAndTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get; set; }

        public string EmpleadoRegistra { get; set; }

    }
}
