using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class Medico : Persona
    {
        public Medico()
        {

        }

        public Medico(int id, String nombre, string segundoNombre, String apellido, String dni, String email, String telefono, DateTime fechaAlta, Usuario usuario, int matriculan, int matriculap, List<Especialidad> especialidades, Boolean activo) : base(id, nombre, segundoNombre, apellido, dni, email, telefono, fechaAlta, usuario)
        {
            MatriculaNacional = matriculan;
            MatriculaProvincial = matriculap;
            EstaActivo = activo;
            Especialidades = especialidades;
        }

        [Required(ErrorMessage = MensajeError.Requerido)]
        [StringLength(8, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int MatriculaNacional{ get; set; }

        //ESTE CAMPO ES OPCIONAL, PUEDE SER NULO
        [StringLength(7, MinimumLength = 5, ErrorMessage = MensajeError.MinMaxString)]
        [RegularExpression(@" ^ [0-9] ", ErrorMessage = MensajeError.NumerosPositivos)]
        public int MatriculaProvincial { get; set; }


        [Required(ErrorMessage = MensajeError.UnaOpcion)]//Al menos 1 Especialidad
        public List<Especialidad> Especialidades { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        public Boolean EstaActivo { get; set; }

        public List<MedicoPaciente> MedicoPacientes { get; set; }
    }
}
