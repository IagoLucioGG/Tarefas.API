using Microsoft.EntityFrameworkCore;
using Tarefas.API.Data;
using Tarefas.API.Domain;
using Tarefas.API.DTO;
using Tarefas.API.Exceptions;
using Tarefas.API.Interface;
using Tarefas.API.Security;

namespace Tarefas.API.Application
{
    public class UsuarioAplication(AppDbContext context, PasswordService passwordService) : IUsuarioAplication
    {
        private readonly AppDbContext _context = context;
        private readonly PasswordService _passwordService = passwordService;

        public async Task<ResponseModel<Usuario>> CadastrarUsuarioAsync(UsuarioRequestDTO dto)
        {
            var loginExistente = await _context.Usuarios.AnyAsync(u => u.NmLogin == dto.NmLogin);

            if (loginExistente)
                throw new ConflictException("Já existe um usuário com esse login.");

            // Cria um usuário temporário apenas para hashear a senha
            var usuarioTemp = new Usuario(dto.NmLogin, string.Empty, dto.Permissao);
            var senhaHash = _passwordService.HashSenha(usuarioTemp, dto.Senha);
            
            // Cria o usuário com a senha já hasheada
            var usuario = new Usuario(dto.NmLogin, senhaHash, dto.Permissao);

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Success(usuario, "Usuário cadastrado com sucesso.");
        }

        public async Task<ResponseModel<Usuario>> InativarUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                throw new NotFoundException($"Usuário não encontrado com esse Id: {idUsuario}");

            usuario.Inativar();
            _context.Update(usuario);

            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Success(usuario, "Usuário inativado com sucesso.");
        }

        public async Task<ResponseModel<Usuario>> AtivarUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                throw new NotFoundException($"Usuário não encontrado com esse Id: {idUsuario}");

            usuario.Ativar();
            _context.Update(usuario);

            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Success(usuario, "Usuário ativado com sucesso.");
        }
    }
}