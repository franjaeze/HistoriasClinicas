using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas.Models;

namespace Historias_Clinicas.Data
{
    public class Historias_ClinicasContext : DbContext
    {
        public Historias_ClinicasContext (DbContextOptions<Historias_ClinicasContext> options)
            : base(options)
        {
        }

        public DbSet<Historias_Clinicas.Models.HistoriaClinica> HistoriaClinica { get; set; }
    }
}
