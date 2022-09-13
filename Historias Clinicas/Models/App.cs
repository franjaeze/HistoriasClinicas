using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class App
    {

        public String Nombre { get; set; }

        public String Direccion { get; set; }

        public String Telefono { get; set; }

        public List<Paciente> Pacientes{ get; set; }

        public List<Medico> Medicos { get; set; }

        public List<Empleado> Empleados { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
