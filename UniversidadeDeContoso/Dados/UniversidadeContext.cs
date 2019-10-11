using Microsoft.EntityFrameworkCore;
using UniversidadeDeContoso.Models;

namespace UniversidadeDeContoso.Dados
{
    public class UniversidadeContext : DbContext
    {
        public UniversidadeContext(DbContextOptions<UniversidadeContext> opcoes) : base(opcoes) {}

        /// <summary>
        /// Por causa da classe Estudante referenciar a classe Materia e ela referenciar Curso,
        /// a classe Materia e Curso poderiam ser omitidas na classe UniversidadeContext
        /// </summary>
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Estudante> Estudantes { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<AtribuicaoSala> AtribuicaoSalas { get; set; }
        public DbSet<AtribuicaoCurso> AtribuicaoCursos { get; set; }


        /// <summary>
        /// Método herdado de DbContext para o alterar os nomes gerados automaticamente por ele;
        /// Que no caso seriam, neste exemplo, os das propriedades acima do tipo DbSet(tabelas)
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>().ToTable("Curso");
            modelBuilder.Entity<Materia>().ToTable("Materia");
            modelBuilder.Entity<Estudante>().ToTable("Estudante");
            modelBuilder.Entity<Departamento>().ToTable("Department");
            modelBuilder.Entity<Professor>().ToTable("Professor");
            modelBuilder.Entity<AtribuicaoSala>().ToTable("OfficeAssignment");
            modelBuilder.Entity<AtribuicaoCurso>().ToTable("CourseAssignment");

            modelBuilder.Entity<AtribuicaoCurso>().HasKey(c => new {c.CursoId, c.ProfessorId});
        }
    }   
}
