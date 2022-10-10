using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Paciente : Persona
    {



        [Required(ErrorMessage = MensajeError.Requerido)]
        public ObraSocial  ObraSocial { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        public HistoriaClinica NroHistoriaClinica { get; set; }


        public List<MedicoPaciente> MedicosPaciente { get; set; }
    }
}
