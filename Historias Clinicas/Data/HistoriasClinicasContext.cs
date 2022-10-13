using Historias_Clinicas.Models;
using Microsoft.EntityFrameworkCore;

namespace Historias_Clinicas.Data
{
    public class HistoriasClinicasContext : DbContext
    {

        public HistoriasClinicasContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MedicoPaciente>().HasKey(mp => new { mp.MedicoId, mp.PacienteId });

            modelBuilder.Entity<MedicoPaciente>()
                .HasOne(mp => mp.Medico)
                .WithMany(m => m.MedicoPacientes)
                .HasForeignKey(mp => mp.MedicoId);


            modelBuilder.Entity<MedicoPaciente>()
                .HasOne(mp => mp.Paciente)
                .WithMany(p => p.MedicosPaciente)
                .HasForeignKey(mp => mp.PacienteId);
        }

        public DbSet<Diagnostico> Diagnosticos { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Epicrisis> Epicrisis { get; set; }

        public DbSet<Episodio> Episodios { get; set; }

        public DbSet<Evolucion> Evoluciones { get; set; }

        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Nota> Notas { get; set; }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
    
}
