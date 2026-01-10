using Microsoft.EntityFrameworkCore;
using Tarefas.API.Data;
using Tarefas.API.Domain;
using Tarefas.API.DTO;
using Tarefas.API.Exceptions;
using Tarefas.API.Secutity;

namespace Tarefas.API.Aplication
{
    public class AuthAplication : IAuthAplication
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly TokenService _tokenService;

    public AuthAplication(
        AppDbContext context,
        PasswordService passwordService,
        TokenService tokenService)
    {
        _context = context;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<ResponseModel<string>> LoginAsync(LoginRequestDTO dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NmLogin == dto.NmLogin);

        if (usuario == null)
            throw new UnauthorizathionException("Usuário não encontrado.");

        if (!usuario.Ativo)
            throw new UnauthorizathionException("Usuário inativo.");

        var senhaValida = _passwordService.VerificarSenha(
            usuario,
            dto.Senha);

        if (!senhaValida)
            throw new UnauthorizathionException("Senha inválida.");

        var token = _tokenService.GerarToken(usuario);

        return ResponseModel<string>.Sucess(token, "Login realizado com sucesso");
    }
}

}