using Tarefas.API.Domain;
using Tarefas.API.DTO;

namespace Tarefas.API.Interface
{
    public interface ITarefaAplication
    {
        Task<ResponseModel<Tarefa>> CadastraTarefaAsync(CriarTarefaRequestDTO dto);
        Task<ResponseModel<Tarefa>> ExecutarTarefa(int idTarefa);
        Task<ResponseModel<Tarefa>> InativaTarefa(int idTarefa);
        Task<ResponseModel<Tarefa>> AtivaTarefa(int idTarefa);
        Task<ResponseModel<List<Tarefa>>> FiltrarTarefasAsync(TarefaFiltroDTO dto);
        Task<ResponseModel<Tarefa>> ConsultarTarefaPorId(int idTarefa);
    }
}