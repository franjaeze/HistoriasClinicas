using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Paciente

    {
        public int Id { get; set; }

        public String Nombre { get; set; }

        
        public String Email { get; set; }

        public String Apellido { get; set; }

        public String Dni { get; set; }

        public  String Telefono { get; set; }

        public DateTime FechaDeAlta { get; set; }

        public String  ObraSocial { get; set; }



    }
}
