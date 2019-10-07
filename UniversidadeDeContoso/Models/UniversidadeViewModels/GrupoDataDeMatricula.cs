using System;
using System.ComponentModel.DataAnnotations;

namespace UniversidadeDeContoso.Models.UniversidadeViewModels
{
    public class GrupoDataDeMatricula
    {
        [DataType(DataType.Date)]
        public DateTime? DataDeMatricula { get; set; }

        public int QuantidadeDeEstudantes { get; set; }
    }
}