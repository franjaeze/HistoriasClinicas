﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class App
    {
        [Required(ErrorMessage = MensajeError.Requerido)]
        public String Nombre { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        public String Direccion { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15,MinimumLength =7, ErrorMessage = MensajeError.MinMaxString)]
        public String Telefono { get; set; }

        public List<Paciente> Pacientes{ get; set; }

        public List<Medico> Medicos { get; set; }

        public List<Empleado> Empleados { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
