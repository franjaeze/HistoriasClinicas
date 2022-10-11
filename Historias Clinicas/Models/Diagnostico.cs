using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Historias_Clinicas.Models
{
    public class Diagnostico
    {

        public Diagnostico() 
         {
         }

        public Diagnostico(int id, int medicoId, String descripcion, String recomendacion, String tratamiento, String estudiosEfectuados, Especialidad especialidad)
        
        {
            MedicoId = medicoId;
            Descripcion = descripcion;
            Recomendacion = recomendacion;
            Tratamiento = tratamiento;
            EstudiosEfectuados = estudiosEfectuados;
            EspecialidadD = especialidad;
        
        }

        public int Id { get; set; }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = MensajeError.MinMaxString)] 
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int MedicoId { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(10000, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        public String Descripcion {get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
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


        [Required(ErrorMessage = MensajeError.Requerido)]
        public Especialidad EspecialidadD { get; set; }
    }
}
