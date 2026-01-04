namespace Tarefas.API.Domain
{
    public class ResponseModel<T>
    {
        public string Mensagem { get; private set; } = string.Empty;
        public bool Sucesso { get; private set; }
        public T? Dados { get; private set; }

        // Sucesso
        public static ResponseModel<T> Sucess(T dados, string? message)
            => new()
            {
                Sucesso = true,
                Mensagem = message,
                Dados = dados,
                
            };

        // Erro
        public static ResponseModel<T> Erro(string message)
            => new()
            {
                Sucesso = false,
                Mensagem = message,
                Dados = default,
            };


    }
}