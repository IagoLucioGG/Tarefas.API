using Tarefas.API.Domain;
using Tarefas.API.DTO;

namespace Tarefas.API.Interface
{
    public interface IUsuarioAplication
    {
        Task<ResponseModel<Usuario>> CadastrarUsuarioAsync(UsuarioRequestDTO dto);
        Task<ResponseModel<Usuario>> InativarUsuario(int idUsuario);
        Task<ResponseModel<Usuario>> AtivarUsuario(int idUsuario);
    }
}