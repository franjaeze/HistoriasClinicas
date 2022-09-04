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

        public DateTime FechaYHora { get; set; }

    }
}
