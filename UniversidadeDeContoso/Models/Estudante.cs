﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public class Estudante
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Column("Sobrenomes")]
        [Display(Name = "Sobrenome")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Sobrenome { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Primeiro nome")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de matrícula")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeMatricula { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto => $"{Nome} {Sobrenome}";

        //Materia eh referenciada aqui em forma de lista, mostrando assim que sua relação e de 1 para N estudantes
        public ICollection<Materia> Materias { get; set; }
    }
}
