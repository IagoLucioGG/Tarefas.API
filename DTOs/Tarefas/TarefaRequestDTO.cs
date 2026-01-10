using System.ComponentModel.DataAnnotations;
using Tarefas.API.Domain;

namespace Tarefas.API.DTO
{
    public record CriarTarefaRequestDTO
    {
        [Required(ErrorMessage = "Descrição da tarefa é obrigatória")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Descrição deve ter entre 3 e 500 caracteres")]
        public string DescTarefa { get; init; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Observação deve ter no máximo 1000 caracteres")]
        public string? Observacao { get; init; }

        [Required(ErrorMessage = "Data para execução é obrigatória")]
        public DateTime DataParaExecucao { get; init; }

        public DateTime? DataExecutada { get; init; }

        [Required(ErrorMessage = "Tipo da tarefa é obrigatório")]
        public TipoTarefa Tipo { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int? QtVezesParaExecucaoPeriodo { get; init; }

        public ModeloPeriodo? Periodo { get; init; }
    }
}
