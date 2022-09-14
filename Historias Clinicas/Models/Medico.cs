﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {

        [Required]
        public int Matricula { get; set; }

        public List<Especialidad> Especialidades { get; set; }

    }
}
