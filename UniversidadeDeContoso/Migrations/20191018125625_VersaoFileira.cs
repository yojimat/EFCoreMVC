using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversidadeDeContoso.Migrations
{
    public partial class VersaoFileira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "VersaoFileira",
                table: "Department",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VersaoFileira",
                table: "Department");
        }
    }
}
