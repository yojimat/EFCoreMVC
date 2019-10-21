using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversidadeDeContoso.Models
{
    public class Estudante : Pessoa
    {
        [DataType(DataType.Date)]
        [Display(Name = "Data de matrícula")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeMatricula { get; set; }

        //Materia eh referenciada aqui em forma de lista, mostrando assim que sua relação e de 1 para N estudantes
        public ICollection<Materia> Materias { get; set; }
    }
}
