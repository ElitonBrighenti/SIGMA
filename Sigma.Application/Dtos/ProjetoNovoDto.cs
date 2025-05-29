using Sigma.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sigma.Domain.Dtos
{
    public class ProjetoNovoDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A Data de Início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A Previsão de Término é obrigatória.")]
        public DateTime PrevisaoTermino { get; set; }

        public DateTime? DataRealTermino { get; set; }

        [Required(ErrorMessage = "O campo Orçamento é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O orçamento deve ser maior que zero.")]
        public decimal Orcamento { get; set; }

        [Required(ErrorMessage = "O campo Risco é obrigatório.")]
        public RiscoProjeto Risco { get; set; }

        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        public StatusProjeto Status { get; set; }
    }

}
