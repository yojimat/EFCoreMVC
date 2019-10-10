using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversidadeDeContoso.Migrations
{
    public partial class CriacaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Curso",
                table => new
                {
                    CursoID = table.Column<int>(),
                    Nome = table.Column<string>(nullable: true),
                    Creditos = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoID);
                });

            migrationBuilder.CreateTable(
                "Estudante",
                table => new
                {
                    ID = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SobreNome = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    DataDeMatricula = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudante", x => x.ID);
                });

            migrationBuilder.CreateTable("Materia",
                table => new
                {
                    MateriaID = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CursoID = table.Column<int>(),
                    EstudanteID = table.Column<int>(),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.MateriaID);

                    table.ForeignKey("FK_Materia_Curso_CursoID", x => x.CursoID, "Curso", "CursoID", onDelete: ReferentialAction.Cascade);

                    table.ForeignKey("FK_Materia_Estudante_EstudanteID", x => x.EstudanteID, "Estudante", "ID", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex("IX_Materia_CursoID", "Materia", "CursoID");

            migrationBuilder.CreateIndex("IX_Materia_EstudanteID", "Materia", "EstudanteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Materia");

            migrationBuilder.DropTable("Curso");

            migrationBuilder.DropTable("Estudante");
        }
    }
}
