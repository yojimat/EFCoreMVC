namespace UniversidadeDeContoso.Models
{
    public class AtribuicaoCurso
    {
        public int ProfessorId { get; set; }
        public int CursoId { get; set; }
        public Professor Professor { get; set; }
        public Curso Curso { get; set; }
    }
}
