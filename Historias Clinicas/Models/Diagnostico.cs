using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Historias_Clinicas.Models
{
    public class Diagnostico
    {
        public int Id { get; set; }

        public int MedicoId { get; set; }

        public String Descripcion {get; set; }

        public String Recomendacion { get; set; }

        public String Tratamiento { get; set; }

        public String EstudiosEfectuados { get; set; }

        public Especialidad Especialidad { get; set; }
    }
}
