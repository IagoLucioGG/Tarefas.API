using Tarefas.API.Domain;

namespace Tarefas.API.DTO
{
    public class TarefaFiltroDTO
    {
        public StatusTarefa? Status { get; init; }
        public TipoTarefa? Tipo { get; init; }
        public ModeloPeriodo? Periodo { get; init; }

        public DateTime? DataExecucaoInicio { get; init; }
        public DateTime? DataExecucaoFim { get; init; }

        public bool? SomenteExecutadas { get; init; }
    }
}
