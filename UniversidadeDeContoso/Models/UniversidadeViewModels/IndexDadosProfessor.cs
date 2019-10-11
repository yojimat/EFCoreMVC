using System.Collections.Generic;

namespace UniversidadeDeContoso.Models.UniversidadeViewModels
{
    public class IndexDadosProfessor
    {
        public IEnumerable<Professor> Professores { get; set; }
        public IEnumerable<Curso> Cursos { get; set; }
        public IEnumerable<Materia> Materias { get; set; }
    }
}
