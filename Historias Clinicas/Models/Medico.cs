using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {

        public int Matricula { get; set; }

        public List<Especialidad> Especialidades { get; set; }

    }
}
