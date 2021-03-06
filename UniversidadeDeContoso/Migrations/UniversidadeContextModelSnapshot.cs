﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversidadeDeContoso.Dados;

namespace UniversidadeDeContoso.Migrations
{
    [DbContext(typeof(UniversidadeContext))]
    partial class UniversidadeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UniversidadeDeContoso.Models.AtribuicaoCurso", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("ProfessorId");

                    b.HasKey("CursoId", "ProfessorId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.AtribuicaoSala", b =>
                {
                    b.Property<int>("ProfessorId");

                    b.Property<string>("Localizacao")
                        .HasMaxLength(50);

                    b.HasKey("ProfessorId");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Curso", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("Creditos");

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Nome")
                        .HasMaxLength(50);

                    b.HasKey("CursoId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataComeco");

                    b.Property<string>("Nome")
                        .HasMaxLength(50);

                    b.Property<decimal>("Orcamento")
                        .HasColumnType("money");

                    b.Property<int?>("ProfessorId");

                    b.Property<byte[]>("VersaoFileira")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("DepartamentoId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Materia", b =>
                {
                    b.Property<int>("MateriaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<int>("EstudanteId");

                    b.Property<int?>("Nota");

                    b.HasKey("MateriaId");

                    b.HasIndex("CursoId");

                    b.HasIndex("EstudanteId");

                    b.ToTable("Materia");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("PrimeiroNome")
                        .HasMaxLength(50);

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Pessoa");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Pessoa");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Estudante", b =>
                {
                    b.HasBaseType("UniversidadeDeContoso.Models.Pessoa");

                    b.Property<DateTime>("DataDeMatricula");

                    b.ToTable("Estudante");

                    b.HasDiscriminator().HasValue("Estudante");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Professor", b =>
                {
                    b.HasBaseType("UniversidadeDeContoso.Models.Pessoa");

                    b.Property<DateTime>("DataContratacao");

                    b.ToTable("Professor");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.AtribuicaoCurso", b =>
                {
                    b.HasOne("UniversidadeDeContoso.Models.Curso", "Curso")
                        .WithMany("AtribuicaoCursos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniversidadeDeContoso.Models.Professor", "Professor")
                        .WithMany("AtribuicaoCursos")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.AtribuicaoSala", b =>
                {
                    b.HasOne("UniversidadeDeContoso.Models.Professor", "Professor")
                        .WithOne("Sala")
                        .HasForeignKey("UniversidadeDeContoso.Models.AtribuicaoSala", "ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Curso", b =>
                {
                    b.HasOne("UniversidadeDeContoso.Models.Departamento", "Departamento")
                        .WithMany("Cursos")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Departamento", b =>
                {
                    b.HasOne("UniversidadeDeContoso.Models.Professor", "Administrador")
                        .WithMany()
                        .HasForeignKey("ProfessorId");
                });

            modelBuilder.Entity("UniversidadeDeContoso.Models.Materia", b =>
                {
                    b.HasOne("UniversidadeDeContoso.Models.Curso", "Curso")
                        .WithMany("Materias")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniversidadeDeContoso.Models.Estudante", "Estudante")
                        .WithMany("Materias")
                        .HasForeignKey("EstudanteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
