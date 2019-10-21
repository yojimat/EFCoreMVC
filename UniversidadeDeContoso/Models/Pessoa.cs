using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversidadeDeContoso.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Required, StringLength(50, ErrorMessage = "Primeiro nome não pode ser maior que 50 caracteres.")]
        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto => $"{Sobrenome}, {Nome}";
    }
}
