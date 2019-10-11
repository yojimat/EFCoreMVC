using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public class Curso
    {
        //Parametro para a chave primaria não ser gerada automaticamente para propriedade CursoID
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Numero")]
        [Key]
        public int CursoId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [Range(0, 5)]
        public int Creditos { get; set; }
        public int DepartamentoId { get; set; }

        public Departamento Departamento { get; set; }
        public ICollection<Materia> Materias { get; set; }
        public ICollection<AtribuicaoCurso> AtribuicaoCursos { get; set; }
    }
}
