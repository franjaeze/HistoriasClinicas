using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Historias_Clinicas.Models
{
    public class Diagnostico
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Medico")]
        public int MedicoId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Descripcion {get; set; }


        
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Recomendacion { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Tratamiento { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        [DataType(DataType.Text)]
        public String EstudiosEfectuados { get; set; }



        public Especialidad Especialidad { get; set; }
    }
}
