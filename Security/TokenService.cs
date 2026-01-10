using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tarefas.API.Domain;

namespace Tarefas.API.Secutity
{
    public class TokenService(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
               new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NmLogin),
                new Claim(ClaimTypes.Role, usuario.Permissao.ToString())
            };

            var secret = _configuration["ChaveSeguranca"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? "CHAVE_SUPER_SECRETA_AQUI"));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}