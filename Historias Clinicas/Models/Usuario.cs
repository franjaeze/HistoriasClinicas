using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Historias_Clinicas.Models
{
    public class Usuario : Persona
    {
        public Usuario()
        {

        }

        public Usuario(int id, String nombre, string segundoNombre, String apellido, String dni, String email, String telefono, DateTime fechaAlta, Usuario usuario, String pass) : base(id, nombre, segundoNombre, apellido, dni, email, telefono, fechaAlta, usuario)
        {
            Password = pass;
         
        }

        
       [Required(ErrorMessage = MensajeError.Requerido)]
       [StringLength(20,MinimumLength =6, ErrorMessage = MensajeError.MinMaxString)]
        public String Password { get; set; }
    }
}
