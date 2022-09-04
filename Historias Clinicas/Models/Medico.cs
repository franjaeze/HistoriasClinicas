using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Medico
    {
        public int Id { get; set; }

        public String Nombre { get; set; }
        public String Apellido { get; set; }

        public String Email { get; set; }

        public String DNI { get; set; }

        public String Telefono { get; set; }

        public String Direccion { get; set; }

        public DateTime FechaAlta { get; set; }

        public int Matricula { get; set; }

    }
}
