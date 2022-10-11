using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Epicrisis
    {

        public Epicrisis ()
        { 
        }

        public Epicrisis (int id , int medicoId, int pacienteId, String resumen, int diasInternacion, DateTime fechaYHora, DateTime fechaAlta, DateTime fechaIngreso, List<Diagnostico> diagnostico )

        {
            MedicoId = medicoId;
            PacienteId = pacienteId;
            Resumen = resumen;
            DiasInternacion = diasInternacion;
            FechaYHora = fechaYHora;
            FechaYHoraAlta = fechaAlta;
            FechaYHoraIngreso = fechaIngreso;
            Diagnosticos = diagnostico; 
        }  
            
         public int Id { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int MedicoId { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Resumen { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int DiasInternacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaYHora { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaYHoraAlta { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaYHoraIngreso { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        public List<Diagnostico> Diagnosticos { get; set; }

    }
}
