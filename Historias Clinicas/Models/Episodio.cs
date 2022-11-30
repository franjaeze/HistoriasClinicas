    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Historias_Clinicas.Helpers;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.Models
{
    public class Episodio
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("HistoriaClinica")]
        [Display(Name = Alias.HistoriaClinicaId)]
        public int HistoriaClinicaId { get; set; }

        
        [ForeignKey("Empleado")]
        [Display(Name = Alias.EmpleadoId)]
        public int EmpleadoId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string Descripcion { get; set; } 


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string Motivo { get; set; } 


        [DataType(DataType.DateTime)]
        [Display(Name = Alias.FechaYHoraInicio)]
        public DateTime FechaYHoraInicio { get; set; } 


        [DataType(DataType.DateTime)]
        [Display(Name = Alias.FechaDeAlta)]
        public DateTime FechaYHoraAlta { get; set; } 


        [DataType(DataType.DateTime)]
        [Display(Name = Alias.FechaYHoraCierre)]
        public DateTime FechaYHoraCierre { get; set; } 

        public Boolean EstadoAbierto { get; set; } 

        public List<Evolucion> Evoluciones { get; set; } 

        public Especialidad Especialidad { get; set; }

    }
}
