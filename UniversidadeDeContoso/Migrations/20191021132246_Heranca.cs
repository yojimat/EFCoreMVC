using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversidadeDeContoso.Migrations
{
    public partial class Heranca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Estudante_EstudanteId",
                table: "Materia");

            migrationBuilder.RenameTable(name: "Professor", newName: "Pessoa");
            migrationBuilder.AddColumn<DateTime>(name: "DataDeMatricula",table: "Pessoa",nullable: true);
            migrationBuilder.AddColumn<string>(name: "Discriminator", table: "Pessoa", nullable: false, maxLength: 128, defaultValue: "Professor");
            migrationBuilder.AlterColumn<DateTime>(name: "DataContratacao",table: "Pessoa",nullable: true);
            migrationBuilder.AddColumn<int>(name: "IdVelho", table: "Pessoa", nullable: true);

            //Copia dados existentes de estudante em uma nova tabela pessoa.
            migrationBuilder.Sql("INSERT INTO dbo.Pessoa (Sobrenome, Nome, DataContratacao, DataDeMatricula, Discriminator, IdVelho) SELECT Sobrenomes, Nome, null AS DataContratacao, DataDeMatricula, 'Estudante' AS Discriminator, Id AS IdVelho FROM dbo.Estudante");
            //Conserta relações existentes para combinar novas PK's.
            migrationBuilder.Sql("UPDATE dbo.Materia SET EstudanteId = (SELECT ID FROM dbo.Pessoa WHERE IdVelho = Materia.EstudanteId AND Discriminator = 'Estudante')");

            // Remove chave temporaria
            migrationBuilder.DropColumn(name: "IdVelho", table: "Pessoa");

            migrationBuilder.DropTable(
                name: "Estudante");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Materia_EstudanteId",
            //    table: "Materia",
            //    column: "EstudanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Pessoa_EstudanteId",
                table: "Materia",
                column: "EstudanteId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignment_Pessoa_ProfessorId",
                table: "CourseAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Pessoa_ProfessorId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Materia_Pessoa_EstudanteId",
                table: "Materia");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeAssignment_Pessoa_ProfessorId",
                table: "OfficeAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoa",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "DataDeMatricula",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pessoa");

            migrationBuilder.RenameTable(
                name: "Pessoa",
                newName: "Professor");

            migrationBuilder.RenameColumn(
                name: "PrimeiroNome",
                table: "Professor",
                newName: "Nome");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataContratacao",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professor",
                table: "Professor",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Estudante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataDeMatricula = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Sobrenomes = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudante", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignment_Professor_ProfessorId",
                table: "CourseAssignment",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Professor_ProfessorId",
                table: "Department",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Materia_Estudante_EstudanteId",
                table: "Materia",
                column: "EstudanteId",
                principalTable: "Estudante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeAssignment_Professor_ProfessorId",
                table: "OfficeAssignment",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
