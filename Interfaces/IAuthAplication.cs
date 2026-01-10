using Tarefas.API.Domain;
using Tarefas.API.DTO;

public interface IAuthAplication
{
    Task<ResponseModel<string>> LoginAsync(LoginRequestDTO dto);
}
