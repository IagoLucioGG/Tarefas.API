using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.DTO;
using Tarefas.API.Interface;

namespace Tarefas.API.Controller
{
    [ApiController]
    [Route("api/{controller}")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAplication _usuarioApp;

        public UsuarioController(IUsuarioAplication usuarioApp)
        {
            _usuarioApp = usuarioApp;
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioRequestDTO dto)
        {
            var response = await _usuarioApp.CadastrarUsuarioAsync(dto);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("inativar/{idUsuario}")]
        public async Task<ActionResult> InativarUsuario(int idUsuario)
        {
            var response = await _usuarioApp.InativarUsuario(idUsuario);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ativar/{idUsuario}")]
        public async Task<ActionResult> AtivarUsuario(int idUsuario)
        {
            var response = await _usuarioApp.AtivarUsuario(idUsuario);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

    }
}