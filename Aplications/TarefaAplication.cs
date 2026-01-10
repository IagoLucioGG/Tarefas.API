using Tarefas.API.Data;
using Tarefas.API.Domain;
using Tarefas.API.DTO;
using Tarefas.API.Interface;
using Microsoft.EntityFrameworkCore;
using Tarefas.API.Exceptions;

namespace Tarefas.API.Application
{
    public class TarefaAplication(AppDbContext context) : ITarefaAplication
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel<Tarefa>> CadastraTarefaAsync(CriarTarefaRequestDTO dto)
        {
            var novaTarefa = new Tarefa(dto.DescTarefa, dto.Observacao ?? "", dto.DataParaExecucao, dto.DataExecutada, dto.Tipo, dto.QtVezesParaExecucaoPeriodo, dto.Periodo);

            await _context.Tarefas.AddAsync(novaTarefa);
            await _context.SaveChangesAsync();

            return ResponseModel<Tarefa>.Success(novaTarefa, "Tarefa Cadastrada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> ExecutarTarefa(int idTarefa)
        {
            var tarefaExecutada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaExecutada == null)
                throw new NotFoundException($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaExecutada.ExecutarTarefa();

            await _context.SaveChangesAsync();

            return ResponseModel<Tarefa>.Success(tarefaExecutada, "Tarefa executada com sucesso.");

        }

        public async Task<ResponseModel<Tarefa>> InativaTarefa(int idTarefa)
        {
            var tarefaInativada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaInativada == null)
                throw new NotFoundException($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaInativada.InativaTarefa();

            await _context.SaveChangesAsync();

            return ResponseModel<Tarefa>.Success(tarefaInativada, "A tarefa foi inativada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> ConsultarTarefaPorId(int idTarefa)
        {
            var tarefa = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefa == null)
                throw new NotFoundException($"Não foi encontrado nenhuma tarefa com este Id {idTarefa}");

            return ResponseModel<Tarefa>.Success(tarefa, "Tarefa encontrada com sucesso.");
        }

        public async Task<ResponseModel<Tarefa>> AtivaTarefa(int idTarefa)
        {
            var tarefaAtivada = await _context.Tarefas.FindAsync(idTarefa);
            if (tarefaAtivada == null)
                throw new NotFoundException($"Não existe tarefa no banco de dados referente este ID {idTarefa}");

            tarefaAtivada.AtivaTarefa();

            await _context.SaveChangesAsync();

            return ResponseModel<Tarefa>.Success(tarefaAtivada, "A tarefa foi Ativada com sucesso.");
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

            return ResponseModel<List<Tarefa>>.Success(tarefas, "Tarefas filtradas com sucesso.");
        }
    }
}