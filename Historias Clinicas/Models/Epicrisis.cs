using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Epicrisis
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public String Resumen { get; set; }

        public int DiasInternacion { get; set; }

        public DateTime FechaYHora { get; set; }

        public DateTime FechaYHoraAlta { get; set; }

        public DateTime FechaYHoraIngreso { get; set; }

        public List<Diagnostico> Diagnostico { get; set; }

    }
}
