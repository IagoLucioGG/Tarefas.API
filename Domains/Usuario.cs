using System.ComponentModel.DataAnnotations;

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
        public string NmLogin { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;
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
            if (string.IsNullOrWhiteSpace(nmLogin) && string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("É necessário fornecer pelo menos o login ou a senha para alteração.");

            if (!string.IsNullOrWhiteSpace(nmLogin))
                NmLogin = nmLogin;

            if (!string.IsNullOrWhiteSpace(senha))
                SenhaHash = senha;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            VerificaInatividade();
            Ativo = false;
        }

        public void AlterarPermissao(TipoRole permissao)
        {
            VerificaInatividade();
            Permissao = permissao;
        }

        private void VerificaInatividade()
        {
            if (!Ativo)
                throw new ArgumentException("Usuário inativo.");
        }
        
        
    }
    

}