using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sobrenome")]
        [Required, StringLength(50)]
        public string Sobrenome { get; set; }

        [Display(Name = "Primeiro nome")]
        [Required, StringLength(50)]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do início do contrato")]
        public DateTime DataContratacao { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        public ICollection<AtribuicaoCurso> AtribuicaoCursos { get; set; }
        public AtribuicaoSala Sala { get; set; }
    }
}
