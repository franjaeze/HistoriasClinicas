using Historias_Clinicas.Helpers;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Evolucion")]
        public int EvolucionId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string Mensaje { get; set; }


        [DataType(DataType.DateTime)]
        [Display(Name = Alias.FechaYHoraInicio)]
        public DateTime FechaYHora { get; set; }

        [ForeignKey("Empleado")]
        [Display(Name = Alias.EmpleadoId)]
        public int EmpleadoId { get; set; }

    }
}
