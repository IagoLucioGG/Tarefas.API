using Tarefas.API.Data;
using Tarefas.API.Domain;
using Tarefas.API.DTO;
using Tarefas.API.Interface;
using Microsoft.EntityFrameworkCore;

namespace Tarefas.API.Aplication
{
    public class TarefaAplication(AppDbContext context) : ITarefaAplication
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel<Tarefa>> CadastraTarefaAsync(CriarTarefaRequestDTO dto)
        {
            var novaTarefa = new Tarefa(dto.DescTarefa, dto.Observacao, dto.DataParaExecucao, dto.DataExecutada, dto.Tipo, dto.QtVezesParaExecucaoPeriodo, dto.Periodo);

            await _context.Tarefas.AddAsync(novaTarefa);
            _context.SaveChanges();

            return ResponseModel<Tarefa>.Sucess(novaTarefa, "Tarefa Cadastrada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> ExecutarTarefa(int idTarefa)
        {
            var tarefaExecutada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaExecutada == null)
                return ResponseModel<Tarefa>.Erro($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaExecutada.ExecutarTarefa();

            _context.SaveChanges();

            return ResponseModel<Tarefa>.Sucess(tarefaExecutada, "Tarefa executada com sucesso.");

        }

        public async Task<ResponseModel<Tarefa>> InativaTarefa(int idTarefa)
        {
            var tarefaInativada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaInativada == null)
                return ResponseModel<Tarefa>.Erro($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaInativada.InativaTarefa();

            _context.SaveChanges();

            return ResponseModel<Tarefa>.Sucess(tarefaInativada, "A tarefa foi inativada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> ConsultarTarefaPorId(int idTarefa)
        {
            var tarefa = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefa != null)
                return ResponseModel<Tarefa>.Erro($"Não foi encontrado nenhuma tarefa com este Id {idTarefa}");

            return ResponseModel<Tarefa>.Sucess(tarefa, "Tarefa encontrada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> AtivaTarefa(int idTarefa)
        {
            var tarefaAtivada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaAtivada == null)
                return ResponseModel<Tarefa>.Erro($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaAtivada.AtivaTarefa();

            _context.SaveChanges();

            return ResponseModel<Tarefa>.Sucess(tarefaAtivada, "A tarefa foi Ativada com sucesso.");
        }

        public async Task<ResponseModel<List<Tarefa>>> FiltrarTarefasAsync(TarefaFiltroDTO dto)
        {
            IQueryable<Tarefa> query = _context.Tarefas.AsQueryable();

            if (dto.Status.HasValue)
                query = query.Where(t => t.Status == dto.Status);

            if (dto.Tipo.HasValue)
                query = query.Where(t => t.Tipo == dto.Tipo);

            if (dto.Periodo.HasValue)
                query = query.Where(t => t.Periodo == dto.Periodo);

            if (dto.SomenteExecutadas.HasValue)
            {
                if (dto.SomenteExecutadas.Value)
                    query = query.Where(t => t.DataExecutada.HasValue);
                else
                    query = query.Where(t => !t.DataExecutada.HasValue);
            }

            if (dto.DataExecucaoInicio.HasValue)
                query = query.Where(t => t.DataExecutada >= dto.DataExecucaoInicio);

            if (dto.DataExecucaoFim.HasValue)
                query = query.Where(t => t.DataExecutada <= dto.DataExecucaoFim);

            var tarefas = await query.ToListAsync();

            return ResponseModel<List<Tarefa>>.Sucess(tarefas, "Tarefas encontradas com sucesso.");
        }
    }
}