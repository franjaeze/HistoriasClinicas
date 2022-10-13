using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Paciente : Persona
    {

        public int Id { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        public Cobertura  ObraSocialP { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        public int HistoriaClincaId { get; set; }


        public List<MedicoPaciente> MedicosPaciente { get; set; }
    }
}
