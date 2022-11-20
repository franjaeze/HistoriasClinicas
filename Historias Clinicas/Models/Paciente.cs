using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas.Models
{
    public class Paciente : Persona
    {
        public Cobertura  ObraSocial { get; set; }


        [ForeignKey("HistoriaClinica")]
        public int? HistoriaClinicaId { get; set; }


        public List<MedicoPaciente> MedicosPaciente { get; set; }


    }
}
