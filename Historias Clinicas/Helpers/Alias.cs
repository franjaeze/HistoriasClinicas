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
        public const string MedicoId = "Nro de Medico";
        public const string PacienteId = "Nro de Paciente";
        public const string Anio = "Año";
        public const string NombreCompleto = "Nombre completo";
        public const string RolName = "Nombre";
        public const string MatriculaNacional = "Matricula Nacional";
        public const string NombreUsuario = "Usuario";
        public const string Activo = "Activo";
        public const string SegundoNombre = "Segundo Nombre";
        public const string FechaDeAlta = "Fecha De Alta";
        public const string HistoriaClinicaId = "Historia Clinica";
        public const string FechaYHoraInicio = "Fecha de Inicio";
        public const string FechaYHoraCierre = "Fecha de Cierre";
        public const string EpicrisisId = "Nro de Epicrisis";
        public const string EmpleadoId = "Nro de Empleado Registró";
        public const string EpisodioId = "Nro de Episodio";
        public const string DiagnosticoId = "Nro de Diagnostico";
        public const string NotaId = "Nro de Nota";
        public const string DescripcionAtencion = "Descripcion Atención";

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

