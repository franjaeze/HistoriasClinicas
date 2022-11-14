using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Historias_Clinicas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MedicosCollections();
            EmpleadosCollections();
            PacienteCollections();

            CreateHostBuilder(args).Build().Run();
        }

        private static void PacienteCollections()
        {
            IList<Paciente> listaPacientes = new List<Paciente>();
        }

        private static void EmpleadosCollections()
        {
            IList<Empleado> listaEmpleados = new List<Empleado>();
        }

        private static void MedicosCollections()
        {
            IList<Medico> listaMedicos = new List<Medico>();

         }

        //private static void MostrarConsola(IList<T> lista)
        //{
        //    foreach(var item in lista)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
