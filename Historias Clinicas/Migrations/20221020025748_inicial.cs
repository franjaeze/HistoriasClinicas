using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Historias_Clinicas.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(maxLength: 40, nullable: false),
                    Telefono = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Epicrisis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(maxLength: 6, nullable: false),
                    PacienteId = table.Column<int>(maxLength: 6, nullable: false),
                    Resumen = table.Column<string>(maxLength: 10000, nullable: false),
                    DiasInternacion = table.Column<int>(nullable: false),
                    FechaYHora = table.Column<DateTime>(nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(nullable: false),
                    FechaYHoraIngreso = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicrisis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasClinicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    AppId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(maxLength: 6, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 10000, nullable: false),
                    Recomendacion = table.Column<string>(maxLength: 10000, nullable: false),
                    Tratamiento = table.Column<string>(maxLength: 10000, nullable: false),
                    EstudiosEfectuados = table.Column<string>(maxLength: 10000, nullable: false),
                    EspecialidadD = table.Column<int>(nullable: false),
                    EpicrisisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Epicrisis_EpicrisisId",
                        column: x => x.EpicrisisId,
                        principalTable: "Epicrisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episodios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(maxLength: 6, nullable: false),
                    MedicoId = table.Column<int>(maxLength: 6, nullable: false),
                    Descripcion = table.Column<string>(maxLength: 10000, nullable: false),
                    Motivo = table.Column<string>(maxLength: 10000, nullable: false),
                    Antecedentes = table.Column<string>(maxLength: 10000, nullable: false),
                    Internacion = table.Column<bool>(nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(nullable: false),
                    FechaYHoraCierre = table.Column<DateTime>(nullable: false),
                    EstadoAbierto = table.Column<bool>(nullable: false),
                    EmpleadoId = table.Column<int>(maxLength: 6, nullable: false),
                    EpicrisisId = table.Column<int>(nullable: true),
                    Especialidad = table.Column<int>(nullable: false),
                    HistoriaClinicaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodios_Epicrisis_EpicrisisId",
                        column: x => x.EpicrisisId,
                        principalTable: "Epicrisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Episodios_HistoriasClinicas_HistoriaClinicaId",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 20, nullable: false),
                    SegundoNombre = table.Column<string>(maxLength: 20, nullable: true),
                    Apellido = table.Column<string>(maxLength: 20, nullable: false),
                    Dni = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(maxLength: 15, nullable: false),
                    FechaDeAlta = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Legajo = table.Column<int>(nullable: true),
                    AppId = table.Column<int>(nullable: true),
                    MatriculaNacional = table.Column<int>(nullable: true),
                    Especialidad = table.Column<int>(nullable: true),
                    Medico_AppId = table.Column<int>(nullable: true),
                    ObraSocialP = table.Column<int>(nullable: true),
                    HistoriaClincaId = table.Column<int>(nullable: true),
                    Paciente_AppId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_App_Medico_AppId",
                        column: x => x.Medico_AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_App_Paciente_AppId",
                        column: x => x.Paciente_AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evoluciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(maxLength: 6, nullable: false),
                    FechaYHoraInicio = table.Column<DateTime>(nullable: false),
                    FechaYHoraAlta = table.Column<DateTime>(nullable: false),
                    FechaYHoraCierre = table.Column<DateTime>(nullable: false),
                    EstadoAbierto = table.Column<bool>(nullable: false),
                    DescripcionAtencion = table.Column<string>(maxLength: 10000, nullable: false),
                    Indicaciones = table.Column<string>(maxLength: 10000, nullable: false),
                    PrecisaEstudiosAdicionales = table.Column<bool>(nullable: false),
                    PrecisaInterconsultaMedica = table.Column<bool>(nullable: false),
                    EpisodioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evoluciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evoluciones_Episodios_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicoPaciente",
                columns: table => new
                {
                    MedicoId = table.Column<int>(maxLength: 6, nullable: false),
                    PacienteId = table.Column<int>(maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoPaciente", x => new { x.MedicoId, x.PacienteId });
                    table.ForeignKey(
                        name: "FK_MedicoPaciente_Personas_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicoPaciente_Personas_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoID = table.Column<string>(maxLength: 6, nullable: false),
                    Mensaje = table.Column<string>(maxLength: 10000, nullable: false),
                    FechaYHora = table.Column<DateTime>(nullable: false),
                    EvolucionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_Evoluciones_EvolucionId",
                        column: x => x.EvolucionId,
                        principalTable: "Evoluciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_EpicrisisId",
                table: "Diagnosticos",
                column: "EpicrisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_EpicrisisId",
                table: "Episodios",
                column: "EpicrisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodios_HistoriaClinicaId",
                table: "Episodios",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evoluciones_EpisodioId",
                table: "Evoluciones",
                column: "EpisodioId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoPaciente_PacienteId",
                table: "MedicoPaciente",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EvolucionId",
                table: "Notas",
                column: "EvolucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_AppId",
                table: "Personas",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Medico_AppId",
                table: "Personas",
                column: "Medico_AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Paciente_AppId",
                table: "Personas",
                column: "Paciente_AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UsuarioId",
                table: "Personas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AppId",
                table: "Usuarios",
                column: "AppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "MedicoPaciente");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Evoluciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Episodios");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropTable(
                name: "Epicrisis");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");
        }
    }
}
