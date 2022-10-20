using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Evolucion 
    {
      
        public int Id { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)] // Puede haber entre 0 y 999.999 medicos
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]  // En cada caracter solo se puede poner numeros de 0 a 9
        public int MedicoId { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaYHoraInicio { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaYHoraAlta { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaYHoraCierre { get; set; }


        public Boolean EstadoAbierto { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string DescripcionAtencion { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string Indicaciones { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        public Boolean PrecisaEstudiosAdicionales { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        public Boolean PrecisaInterconsultaMedica { get; set; }


        public List<Nota> Notas { get; set; }
    }
}
