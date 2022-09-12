﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Historias_Clinicas.Models
{
    public class Evolucion 
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }

        public DateAndTime FechaYHoraInicio { get; set; }

        public DateAndTime FechaYHoraAlta { get; set; }

        public DateAndTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get; set; }

        public string DescripcionAtencion { get; set; }

        public List<Nota> Notas { get; set; }
    }
}
