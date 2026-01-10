using System.ComponentModel.DataAnnotations;
using Tarefas.API.Secutity;

namespace Tarefas.API.Domain
{

    public enum TipoRole
    {
        Admin = 1,
        Comum = 2
    }
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; private set; }
        public string NmLogin { get; private set; }
        public string SenhaHash { get; private set; }
        public bool Ativo { get; private set; } = true;
        public TipoRole Permissao { get; private set; }
        public DateTime DataCriacao { get; init; }

        protected Usuario() { }

        public Usuario(string nmLogin, string senha, TipoRole permissao)
        {

            NmLogin = nmLogin;
            Permissao = permissao;
            SenhaHash = senha;
            Ativo = true;
            DataCriacao = DateTime.UtcNow;

        }

        public void AlterarLogin(string? nmLogin, string? senha)
        {
            if (!string.IsNullOrWhiteSpace(nmLogin))
                NmLogin = nmLogin;

            if (!string.IsNullOrWhiteSpace(senha))
                SenhaHash = senha;

            if (!string.IsNullOrWhiteSpace(nmLogin) && !string.IsNullOrWhiteSpace(senha))
                throw new ArgumentNullException("Não é possivel alterar os dados de login ou senha, sem nenhuma informação.");
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            verificaInatividade();
            Ativo = false;
        }

        public void AlterarPermissao(TipoRole permissao)
        {
            verificaInatividade();
            Permissao = permissao;
        }

        public void verificaInatividade()
        {
            if (!Ativo)
                throw new ArgumentException("Usuário inativo.");
        }
        
        
    }
    

}