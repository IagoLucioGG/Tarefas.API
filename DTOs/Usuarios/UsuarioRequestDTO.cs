using Tarefas.API.Domain;

namespace Tarefas.API.DTO
{
    public record UsuarioRequestDTO(
        string NmLogin,
        string Senha,
        TipoRole Permissao
    );
}