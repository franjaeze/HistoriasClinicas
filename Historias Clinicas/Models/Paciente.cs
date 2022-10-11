using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Historias_Clinicas.Models
{
    public class Paciente : Persona
    {

        public Paciente() 
         {
         
         }

       public Paciente (int id, String nombre, string segundoNombre,  String apellido, String dni, String email, String telefono, DateTime fechaAlta, Usuario usuario, ObraSocial obraSocial, int historiaClincaId): base(id, nombre, segundoNombre,  apellido, dni, email, telefono, fechaAlta, usuario)
        {  
            ObraSocialP = obraSocial;
             HistoriaClincaId = historiaClincaId;
            
            }
        
          

        [Required(ErrorMessage = MensajeError.Requerido)]
        public ObraSocial  ObraSocialP { get; set; }


        [Required(ErrorMessage = MensajeError.Requerido)]
        public int HistoriaClincaId { get; set; }


        public List<MedicoPaciente> MedicosPaciente { get; set; }
    }
}
