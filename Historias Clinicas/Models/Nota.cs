using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MedicoID { get; set; }

        public string Mensaje { get; set; }

        public DateAndTime FechaYHora { get; set; }

    }
}
