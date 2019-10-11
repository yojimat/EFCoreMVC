using System.ComponentModel.DataAnnotations;

namespace UniversidadeDeContoso.Models
{
    public class AtribuicaoSala
    {
        [Key]
        public int ProfessorId { get; set; }

        [StringLength(50)]
        [Display(Name = "Localização da sala")]
        public string Localizacao { get; set; }

        public Professor Professor { get; set; }
    }
}
