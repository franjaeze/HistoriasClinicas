using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Usuario
    {

        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }

        public String Email { get; set; }

        public DateTime FechaAlta { get; set; }

        public String Password { get; set; }
    }
}
