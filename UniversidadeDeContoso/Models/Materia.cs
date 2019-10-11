using System.ComponentModel.DataAnnotations;

namespace UniversidadeDeContoso.Models
{
    public enum Nota
    {
        A, B, C, D, F
    }
    public class Materia
    {
        public int MateriaId { get; set; }
        public int CursoId { get; set; }
        public int EstudanteId { get; set; }

        [DisplayFormat(NullDisplayText = "Nota não atribuída")]
        public Nota? Nota { get; set; }

        public Curso Curso { get; set; }
        public Estudante Estudante { get; set; }
    }
}
