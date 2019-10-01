using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public class Curso
    {
        //Parametro para a chave primaria não ser gerada automaticamente a propriedade CursoID
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoID { get; set; }
        public string Nome { get; set; }
        public int Creditos { get; set; }

        public ICollection<Materia> Materias { get; set; }
    }
}
