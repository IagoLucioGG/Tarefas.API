using Microsoft.AspNetCore.Identity;
using Tarefas.API.Domain;

namespace Tarefas.API.Secutity
{
    public class PasswordService
{
    private readonly PasswordHasher<Usuario> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    public string HashSenha(Usuario usuario, string senha)
    {
        return _passwordHasher.HashPassword(usuario, senha);
    }

    public bool VerificarSenha(Usuario usuario, string senhaInformada)
    {
        var resultado = _passwordHasher.VerifyHashedPassword(
            usuario,
            usuario.SenhaHash,
            senhaInformada);

        return resultado == PasswordVerificationResult.Success;
    }
}
}