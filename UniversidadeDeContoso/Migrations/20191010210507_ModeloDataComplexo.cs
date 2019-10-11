using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversidadeDeContoso.Migrations
{
    public partial class ModeloDataComplexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Curso_CursoID",
                table: "Materia");

            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Estudante_EstudanteID",
                table: "Materia");

            migrationBuilder.RenameColumn(
                name: "EstudanteID",
                table: "Materia",
                newName: "EstudanteId");

            migrationBuilder.RenameColumn(
                name: "CursoID",
                table: "Materia",
                newName: "CursoId");

            migrationBuilder.RenameColumn(
                name: "MateriaID",
                table: "Materia",
                newName: "MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_EstudanteID",
                table: "Materia",
                newName: "IX_Materia_EstudanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_CursoID",
                table: "Materia",
                newName: "IX_Materia_CursoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Estudante",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SobreNome",
                table: "Estudante",
                newName: "Sobrenomes");

            migrationBuilder.RenameColumn(
                name: "CursoID",
                table: "Curso",
                newName: "CursoId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estudante",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sobrenomes",
                table: "Estudante",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Curso",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "DepartamentoId",
            //    table: "Curso",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Sobrenome = table.Column<string>(maxLength: 50, nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    DataContratacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseAssignment",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAssignment", x => new { x.CursoId, x.ProfessorId });
                    table.ForeignKey(
                        name: "FK_CourseAssignment_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseAssignment_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: true),
                    Orcamento = table.Column<decimal>(type: "money", nullable: false),
                    DataComeco = table.Column<DateTime>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartamentoId);
                    table.ForeignKey(
                        name: "FK_Department_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("INSERT INTO dbo.Department (Nome, Orcamento, DataComeco) VALUES ('Temp', 0.00, GETDATE())");
            // Default value for FK points to department created above, with
            // defaultValue changed to 1 in following AddColumn statement.

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Curso",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "OfficeAssignment",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(nullable: false),
                    Localizacao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeAssignment", x => x.ProfessorId);
                    table.ForeignKey(
                        name: "FK_OfficeAssignment_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_DepartamentoId",
                table: "Curso",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignment_ProfessorId",
                table: "CourseAssignment",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ProfessorId",
                table: "Department",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Department_DepartamentoId",
                table: "Curso",
                column: "DepartamentoId",
                principalTable: "Department",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Curso_CursoId",
                table: "Materia",
                column: "CursoId",
                principalTable: "Curso",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Estudante_EstudanteId",
                table: "Materia",
                column: "EstudanteId",
                principalTable: "Estudante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Department_DepartamentoId",
                table: "Curso");

            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Curso_CursoId",
                table: "Materia");

            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Estudante_EstudanteId",
                table: "Materia");

            migrationBuilder.DropTable(
                name: "CourseAssignment");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "OfficeAssignment");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Curso_DepartamentoId",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Curso");

            migrationBuilder.RenameColumn(
                name: "EstudanteId",
                table: "Materia",
                newName: "EstudanteID");

            migrationBuilder.RenameColumn(
                name: "CursoId",
                table: "Materia",
                newName: "CursoID");

            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "Materia",
                newName: "MateriaID");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_EstudanteId",
                table: "Materia",
                newName: "IX_Materia_EstudanteID");

            migrationBuilder.RenameIndex(
                name: "IX_Materia_CursoId",
                table: "Materia",
                newName: "IX_Materia_CursoID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Estudante",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Sobrenomes",
                table: "Estudante",
                newName: "SobreNome");

            migrationBuilder.RenameColumn(
                name: "CursoId",
                table: "Curso",
                newName: "CursoID");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estudante",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SobreNome",
                table: "Estudante",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Curso",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Curso_CursoID",
                table: "Materia",
                column: "CursoID",
                principalTable: "Curso",
                principalColumn: "CursoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Estudante_EstudanteID",
                table: "Materia",
                column: "EstudanteID",
                principalTable: "Estudante",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
