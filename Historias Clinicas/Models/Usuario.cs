using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Usuario : Persona
    {

    
        public DateTime FechaAlta { get; set; }

        public String Password { get; set; }
    }
}
