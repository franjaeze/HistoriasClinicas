using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Historias_Clinicas.Helpers;
using Microsoft.VisualBasic;

namespace Historias_Clinicas.Models
{
    public class Diagnostico
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Epicrisis")]
        [Display(Name = Alias.EpicrisisId)]
        public int EpicrisisId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Descripcion {get; set; }

      
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Recomendacion { get; set; }
    }
}
