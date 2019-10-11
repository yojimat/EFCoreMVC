using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public class Departamento
    {
        [Key]
        public int DepartamentoId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Orcamento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de começo")]
        public DateTime DataComeco { get; set; }

        public int? ProfessorId { get; set; }

        public Professor Administrador { get; set; }
        public ICollection<Curso> Cursos { get; set; }
    }
}
