using Microsoft.EntityFrameworkCore;
using Tarefas.API.Data;
using Tarefas.API.Domain;
using Tarefas.API.DTO;
using Tarefas.API.Exceptions;
using Tarefas.API.Interface;

namespace Tarefas.API.Aplication
{
    public class UsuarioAplication(AppDbContext context) : IUsuarioAplication
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel<Usuario>> CadastrarUsuarioAsync(UsuarioRequestDTO dto)
        {
            var loginExistente = await _context.Usuarios.AnyAsync(u => u.NmLogin == dto.NmLogin);

            if (loginExistente)
                throw new ConflictException("Já existe um usuário com esse login.");

            var usuario = new Usuario(dto.NmLogin, dto.Senha, dto.Permissao);

            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Sucess(usuario, "Usuário cadastrado com sucesso.");
        }

        public async Task<ResponseModel<Usuario>> InativarUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                throw new NotFoundException($"Usuário não encontrado com esse Id: {idUsuario}");

            usuario.Inativar();
            _context.Update(usuario);

            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Sucess(usuario, "Usuário inativado com sucesso.");
        }

        public async Task<ResponseModel<Usuario>> AtivarUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                throw new NotFoundException($"Usuário não encontrado com esse Id: {idUsuario}");

            usuario.Ativar();
            _context.Update(usuario);

            await _context.SaveChangesAsync();

            return ResponseModel<Usuario>.Sucess(usuario, "Usuário ativado com sucesso.");
        }
    }
}