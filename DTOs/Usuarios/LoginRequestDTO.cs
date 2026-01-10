using System.ComponentModel.DataAnnotations;

namespace Tarefas.API.DTO
{
    public record LoginRequestDTO
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Login deve ter entre 3 e 100 caracteres")]
        public string NmLogin { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Senha deve ter entre 6 e 100 caracteres")]
        public string Senha { get; init; } = string.Empty;
    }
}