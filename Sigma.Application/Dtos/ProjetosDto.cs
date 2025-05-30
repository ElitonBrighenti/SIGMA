using Sigma.Domain.Enums;

namespace Sigma.Application.Dtos
{
    public class ProjetosDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime PrevisaoTermino { get; set; }
        public DateTime? DataRealTermino { get; set; }
        public decimal Orcamento { get; set; }
        public RiscoProjeto Risco { get; set; }
        public StatusProjeto Status { get; set; }
    }

}
