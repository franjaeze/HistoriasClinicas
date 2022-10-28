﻿using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Historias_Clinicas.Data
{
    public class HistoriasClinicasContext : IdentityDbContext <IdentityUser<int>, IdentityRole<int>, int>
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

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");



        }

        //private void WillCascadeOnDelete(bool v)
        //{
        //    throw new NotImplementedException();
        //}

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

        public DbSet<Historias_Clinicas.Models.App> App { get; set; }

        public DbSet<Rol> Roles { get; set; }

    }
    
}
