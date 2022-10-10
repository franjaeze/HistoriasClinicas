using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.Models
{
    public class Episodio
    {

        public int Id { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)] // Puede haber entre 0 y 999.999 medicos
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]  // En cada caracter solo se puede poner numeros de 0 a 9
        public int PacienteId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campoeste campo
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)] // Puede haber entre 0 y 999.999 medicos
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]  // En cada caracter solo se puede poner numeros de 0 a 9
        public int MedicoId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string Descripcion { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public String Motivo { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public String Antecedentes { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        public Boolean Internacion { get; set; }


        [DataType(DataType.Date)]
        public DateAndTime FechaYHoraInicio { get; set; }


        [DataType(DataType.Date)]
        public DateAndTime FechaYHoraAlta { get; set; }


        [DataType(DataType.Date)]
        public DateAndTime FechaYHoraCierre { get; set; }


        public Boolean EstadoAbierto { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)] // Puede haber entre 0 y 999.999 medicos
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]  // En cada caracter solo se puede poner numeros de 0 a 9
        public int EmpleadoId { get; set; }


        public List<Evolucion> Evoluciones { get; set; }

        public Epicrisis Epicrisis { get; set; }

        public Especialidad Especialidad { get; set; }

    }
}
