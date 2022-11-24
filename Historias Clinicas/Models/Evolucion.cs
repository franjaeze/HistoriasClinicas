using Historias_Clinicas.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_Clinicas.Models
{
    public class Evolucion 
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Episodio")]
        [Display(Name = Alias.EpisodioId)]
        public int EpisodioId { get; set; }


        [ForeignKey("Medico")]
        [Display(Name = Alias.MedicoId)]
        public int MedicoId { get; set; } 


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


        [Required(ErrorMessage = MensajeError.Requerido)] // Es un requerimiento completar este campo
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)] //Minimo 5 caracteres con maximo 10000
        public string DescripcionAtencion { get; set; } 

        public List<Nota> Notas { get; set; } 
    }
}
