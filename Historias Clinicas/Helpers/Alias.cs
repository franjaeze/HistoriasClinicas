using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas.Helpers
{
    public class Alias
    {
        
        public const string DNI = "DNI";
        public const string Email = "Correo electrónico";
        public const string PersonaId = "Persona";
        public const string Password = "Contraseña";
        public const string PassConfirm = "Confirmación de contraseña";
        public const string MedicoId = "Medico";
        public const string EmpleadoId = "Empleado";
        public const string PacienteId = "Paciente";
        public const string Anio = "Año";
        public const string NombreCompleto = "Nombre completo";
        public const string RolName = "Nombre";
        public const string MatriculaNacional = "Matricula Nacional";
        public const string NombreUsuario = "Usuario";
        public const string Activo = "Activo";
        public const string SegundoNombre = "Segundo Nombre";
        public const string FechaDeAlta = "Fecha De Alta";

    }

    public static class AliasGUI
    {
        public static string Create { get { return "Crear"; } }
        public static string Delete { get { return "Eliminar"; } }
        public static string Edit { get { return "Editar"; } }
        public static string Details { get { return "Detalles"; } }
        public static string Back { get { return "Volver atras"; } }
        public static string BackToList { get { return "Volver al listado"; } }
        public static string SureToDelete { get { return "¿Está seguro que desa proceder con la eliminación?"; } }
        public static string ListOf { get { return "Listado de "; } }

        public static string Save { get { return "Guardar"; } }
    }
}

