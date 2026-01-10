namespace Tarefas.API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mensagem) : base(mensagem) { }
    }

    public class UnauthorizathionException : Exception
    {
        public UnauthorizathionException(string mensagem) : base(mensagem) { }
    }
    
    public class ConflictException : Exception
    {
        public ConflictException(string mensagem) : base(mensagem){ }
    }
}