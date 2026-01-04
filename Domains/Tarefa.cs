using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Tarefas.API.Domain
{

    public enum TipoTarefa
    {
        Unica = 1,
        Recorrente = 2
    }

    public enum StatusTarefa
    {
        Ativa = 1,
        Inativa = 2
    }

    public enum ModeloPeriodo
    {
        Dia = 1,
        Semana = 2,
        Mes = 3,
        Ano = 4
    }
    public class Tarefa
    {
        [Key]
        public int IdTarefa { get; set; }
        public string DescTarefa { get; private set; } = string.Empty;
        public StatusTarefa Status { get; private set; }
        public string Observacao { get; private set; }
        public TipoTarefa Tipo { get; private set; } = TipoTarefa.Unica;
        public ModeloPeriodo? Periodo { get; private set; }
        public int? QtVezesParaExecucaoPeriodo { get; private set; }
        public int? QtVezesExecutadaPeriodo { get; private set; }
        public DateTime? DataParaExecucao { get; private set; }
        public DateTime? DataExecutada { get; private set; }
        public DateTime DataCriacao { get; } = DateTime.UtcNow;
        public DateTime? DataUltimaAlteracao { get; private set; }

        //Contrutor para o EF
        protected Tarefa() { }

        // Contrutor publico
        public Tarefa(string Desc, string Obs, DateTime DtExecucao, DateTime? DtExecutada, TipoTarefa TpTarefa, int? qtVezesExecucao, ModeloPeriodo? periodo)
        {
            //Verificação se a Descrição da Tarefa está preenchida corretamente.
            if (string.IsNullOrWhiteSpace(Desc))
                throw new ArgumentException("Descrição da tarefa inválida, vazia ou sem contexto. Por favor verifique!");

            // Verificação se a data que a tarefa foi executada é menor que a data de agendamento da execução.
            if (DtExecutada.HasValue && DtExecutada < DtExecucao)
                throw new ArgumentException("A data de execução da tarefa, não pode ser menor que a data que foi agendada.");

            //Verificação se a data de agendamento da execução é menor que a data Atual.
            if (DtExecucao < DateTime.UtcNow && !DtExecutada.HasValue)
                throw new ArgumentException("A data de execução da tarefa não pode ser menor que a data atual, sem que já tenha sido executada.");

            if (DtExecutada.HasValue && TpTarefa == TipoTarefa.Recorrente)
                throw new ArgumentException("Não pode ser informado data, para uma tarefa recorrente.");

            if (qtVezesExecucao.HasValue && TpTarefa == TipoTarefa.Unica)
                throw new ArgumentException("Tarefas unicas, não podem ter quantidade de vezes que vão executar, pois serão executadas apenas uma vez.");

            if (TpTarefa == TipoTarefa.Recorrente && (!qtVezesExecucao.HasValue || !periodo.HasValue))
                throw new ArgumentException("Tarefas recorrentes precisam ter a quantidade de vezes que seram executadas, em que periodo.");

            DescTarefa = Desc;
            Observacao = Obs;
            DataParaExecucao = DtExecucao;
            Tipo = TpTarefa;
            Status = StatusTarefa.Ativa;

            if (qtVezesExecucao.HasValue)
                QtVezesParaExecucaoPeriodo = qtVezesExecucao;

            if (periodo.HasValue)
                Periodo = (ModeloPeriodo)periodo;

            //Verificação se a data que foi executada é existente na construção, e o tipo da tarefa é único, se sim, é preenchido corretamente, se não é criada sem a data de realização da execução.
            if (DtExecutada.HasValue && TpTarefa == TipoTarefa.Unica)
                DataExecutada = (DateTime)DtExecutada;


        }

        // Método para inativação de uma tarefa.
        public void InativaTarefa()
        {
            if (Tipo == TipoTarefa.Unica && DataExecutada.HasValue)
                throw new ArgumentException($"Não pode ser alterado o status de uma tarefa unica, que já foi executada. Data da Execução: {DataExecutada}");

            Status = StatusTarefa.Inativa;
            DataUltimaAlteracao = DateTime.UtcNow;
        }

        //Metodo para ativação de uma tarefa.
        public void AtivaTarefa()
        {
            Status = StatusTarefa.Ativa;
            DataUltimaAlteracao = DateTime.UtcNow;
        }

        public void AlteraDadosTarefa(string Desc, string Obs, TipoTarefa tp)
        {
            // Ainda vou definir os dados que serão atualizados aqui.
            DataUltimaAlteracao = DateTime.UtcNow;
        }

        public void ExecutarTarefa()
        {

            if (Status == StatusTarefa.Inativa)
                throw new ArgumentException("Não é possível executar uma tarefa inativa.");

            if (Tipo == TipoTarefa.Unica)
            {
                if (DataExecutada.HasValue)
                    throw new ArgumentException($"Está tarefa já foi realizada no dia {DataExecutada}");

                DataExecutada = DateTime.UtcNow;
                DataUltimaAlteracao = DateTime.UtcNow;
                return;
            }

            else if (Tipo == TipoTarefa.Recorrente)
            {

                if (!QtVezesParaExecucaoPeriodo.HasValue)
                    throw new InvalidOperationException("Tarefa recorrente sem limite de execuções definido.");

                var agora = DateTime.UtcNow;

                // Se nunca executou, inicia contagem
                if (!DataExecutada.HasValue)
                {
                    QtVezesExecutadaPeriodo = 0;
                }
                else
                {
                    // Verifica se mudou o período
                    if (PeriodoMudou(DataExecutada.Value, agora))
                    {
                        QtVezesExecutadaPeriodo = 0;
                    }
                }

                if (QtVezesExecutadaPeriodo >= QtVezesParaExecucaoPeriodo)
                    throw new ArgumentException("Limite de execuções para o período atual já foi atingido.");

                QtVezesExecutadaPeriodo++;
                DataExecutada = agora;
                DataUltimaAlteracao = agora;

                DataExecutada = DateTime.UtcNow;
                DataUltimaAlteracao = DateTime.UtcNow;
            }
        }
        private bool PeriodoMudou(DateTime ultimaExecucao, DateTime agora)
        {
            return Periodo switch
            {
                ModeloPeriodo.Dia =>
                    ultimaExecucao.Date != agora.Date,

                ModeloPeriodo.Semana =>
                    ISOWeek.GetWeekOfYear(ultimaExecucao) != ISOWeek.GetWeekOfYear(agora)
                    || ultimaExecucao.Year != agora.Year,

                ModeloPeriodo.Mes =>
                    ultimaExecucao.Month != agora.Month
                    || ultimaExecucao.Year != agora.Year,

                ModeloPeriodo.Ano =>
                    ultimaExecucao.Year != agora.Year,

                _ => throw new InvalidOperationException("Modelo de período inválido.")
            };
        }
    }
}