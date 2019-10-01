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
        }
    }   
}
