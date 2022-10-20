﻿// <auto-generated />
using System;
using Historias_Clinicas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Historias_Clinicas.Migrations
{
    [DbContext(typeof(HistoriasClinicasContext))]
    partial class HistoriasClinicasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Historias_Clinicas.Models.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.ToTable("App");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Diagnostico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int?>("EpicrisisId")
                        .HasColumnType("int");

                    b.Property<int>("EspecialidadD")
                        .HasColumnType("int");

                    b.Property<string>("EstudiosEfectuados")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int>("MedicoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<string>("Recomendacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("Tratamiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.HasKey("Id");

                    b.HasIndex("EpicrisisId");

                    b.ToTable("Diagnosticos");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Epicrisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DiasInternacion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<int>("PacienteId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<string>("Resumen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.HasKey("Id");

                    b.ToTable("Epicrisis");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Episodio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Antecedentes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<int?>("EpicrisisId")
                        .HasColumnType("int");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaYHoraAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraCierre")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HistoriaClinicaId")
                        .HasColumnType("int");

                    b.Property<bool>("Internacion")
                        .HasColumnType("bit");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int>("PacienteId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.HasIndex("EpicrisisId");

                    b.HasIndex("HistoriaClinicaId");

                    b.ToTable("Episodios");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Evolucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescripcionAtencion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int?>("EpisodioId")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoAbierto")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaYHoraAlta")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraCierre")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaYHoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Indicaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.Property<int>("MedicoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<bool>("PrecisaEstudiosAdicionales")
                        .HasColumnType("bit");

                    b.Property<bool>("PrecisaInterconsultaMedica")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EpisodioId");

                    b.ToTable("Evoluciones");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.HistoriaClinica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HistoriasClinicas");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.MedicoPaciente", b =>
                {
                    b.Property<int>("MedicoId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.Property<int>("PacienteId")
                        .HasColumnType("int")
                        .HasMaxLength(6);

                    b.HasKey("MedicoId", "PacienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("MedicoPaciente");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Nota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EvolucionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("MedicoID")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(10000);

                    b.HasKey("Id");

                    b.HasIndex("EvolucionId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaDeAlta")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("SegundoNombre")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Personas");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persona");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Empleado", b =>
                {
                    b.HasBaseType("Historias_Clinicas.Models.Persona");

                    b.Property<int?>("AppId")
                        .HasColumnType("int");

                    b.Property<int>("Legajo")
                        .HasColumnType("int");

                    b.HasIndex("AppId");

                    b.HasDiscriminator().HasValue("Empleado");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Medico", b =>
                {
                    b.HasBaseType("Historias_Clinicas.Models.Persona");

                    b.Property<int?>("AppId")
                        .HasColumnName("Medico_AppId")
                        .HasColumnType("int");

                    b.Property<int>("Especialidad")
                        .HasColumnType("int");

                    b.Property<int>("MatriculaNacional")
                        .HasColumnType("int");

                    b.HasIndex("AppId");

                    b.HasDiscriminator().HasValue("Medico");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Paciente", b =>
                {
                    b.HasBaseType("Historias_Clinicas.Models.Persona");

                    b.Property<int?>("AppId")
                        .HasColumnName("Paciente_AppId")
                        .HasColumnType("int");

                    b.Property<int>("HistoriaClincaId")
                        .HasColumnType("int");

                    b.Property<int>("ObraSocialP")
                        .HasColumnType("int");

                    b.HasIndex("AppId");

                    b.HasDiscriminator().HasValue("Paciente");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Diagnostico", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Epicrisis", null)
                        .WithMany("Diagnosticos")
                        .HasForeignKey("EpicrisisId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Episodio", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Epicrisis", "Epicrisis")
                        .WithMany()
                        .HasForeignKey("EpicrisisId");

                    b.HasOne("Historias_Clinicas.Models.HistoriaClinica", null)
                        .WithMany("Episodios")
                        .HasForeignKey("HistoriaClinicaId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Evolucion", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Episodio", null)
                        .WithMany("Evoluciones")
                        .HasForeignKey("EpisodioId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.MedicoPaciente", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Medico", "Medico")
                        .WithMany("MedicoPacientes")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Historias_Clinicas.Models.Paciente", "Paciente")
                        .WithMany("MedicosPaciente")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Nota", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Evolucion", null)
                        .WithMany("Notas")
                        .HasForeignKey("EvolucionId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Persona", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Usuario", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.App", null)
                        .WithMany("Usuarios")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Empleado", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.App", null)
                        .WithMany("Empleados")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Medico", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.App", null)
                        .WithMany("Medicos")
                        .HasForeignKey("AppId");
                });

            modelBuilder.Entity("Historias_Clinicas.Models.Paciente", b =>
                {
                    b.HasOne("Historias_Clinicas.Models.App", null)
                        .WithMany("Pacientes")
                        .HasForeignKey("AppId");
                });
#pragma warning restore 612, 618
        }
    }
}
