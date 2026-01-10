using Tarefas.API.Domain;
using Tarefas.API.DTO;

namespace Tarefas.API.Interface
{
    public interface IAuthAplication
    {
        Task<ResponseModel<string>> LoginAsync(LoginRequestDTO dto);
    }
}
