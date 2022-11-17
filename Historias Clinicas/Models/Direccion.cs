using Historias_Clinicas.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Direccion
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        public string Calle { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [RegularExpression("([0-9]+)", ErrorMessage = MensajeError.SoloNumeros)]
        public string Altura { get; set; }


        public string Piso { get; set; }


        public string Departamento { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = MensajeError.MinMaxString)]
        public string Localidad { get; set; }
    }
}
