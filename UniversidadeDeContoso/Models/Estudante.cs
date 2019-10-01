using System;
using System.Collections.Generic;

namespace UniversidadeDeContoso.Models
{
    public class Estudante
    {
        public int ID { get; set; }
        public string SobreNome { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeMatricula { get; set; }

        //Materia eh referenciada aqui em forma de lista, mostrando assim que sua relação e de 1 para N estudantes
        public ICollection<Materia> Materias { get; set; }
    }
}
