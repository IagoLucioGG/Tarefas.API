namespace Tarefas.API.DTO
{
    public record LoginRequestDTO(
        string NmLogin,
        string Senha
    );
}