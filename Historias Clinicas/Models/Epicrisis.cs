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

        public Diagnostico Diagnostico { get; set; }

        [ForeignKey("Episodio")]
        public int EpisodioId { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime FechaYHora { get; set; }

    }
}
