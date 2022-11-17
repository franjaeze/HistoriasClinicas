using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class Epicrisis
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Medico")]
        public int MedicoId { get; set; }


        [ForeignKey("Episodio")]
        public int EpisodioId { get; set; }


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
        public DateTime FechaYHoraAlta{ get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaYHoraIngreso { get; set; }



        public List<Diagnostico> Diagnosticos { get; set; }

    }
}
