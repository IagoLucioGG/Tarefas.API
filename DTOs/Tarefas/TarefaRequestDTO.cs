using Tarefas.API.Domain;

namespace Tarefas.API.DTO
{
    public record CriarTarefaRequestDTO(
        string DescTarefa,
        string? Observacao,
        DateTime DataParaExecucao,
        DateTime? DataExecutada,
        TipoTarefa Tipo,
        int? QtVezesParaExecucaoPeriodo,
        ModeloPeriodo? Periodo
    );
}
