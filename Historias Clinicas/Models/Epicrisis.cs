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


        [ForeignKey("Diagnostico")]
        public int DiagnosticoId { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaYHora { get; set; }

    }
}
