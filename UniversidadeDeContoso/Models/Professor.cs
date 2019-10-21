using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversidadeDeContoso.Models
{
    public class Professor : Pessoa
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do início do contrato")]
        public DateTime DataContratacao { get; set; }

        public ICollection<AtribuicaoCurso> AtribuicaoCursos { get; set; }
        public AtribuicaoSala Sala { get; set; }
    }
}
