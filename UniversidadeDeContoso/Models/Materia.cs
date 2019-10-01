namespace UniversidadeDeContoso.Models
{
    public enum Nota
    {
        A, B, C, D, F
    }
    public class Materia
    {
        public int MateriaID { get; set; }
        public int CursoID { get; set; }
        public int EstudanteID { get; set; }
        public Nota? Nota { get; set; }

        public Curso Curso { get; set; }
        public Estudante Estudante { get; set; }
    }
}
