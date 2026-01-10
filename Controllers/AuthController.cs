using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.DTO;
using Tarefas.API.Interface;

namespace Tarefas.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthAplication _authApp;
        public AuthController(IAuthAplication authApp)
        {
            _authApp = authApp;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginRequestDTO dto)
        {
            var response = await _authApp.LoginAsync(dto);
            if (!response.Sucesso)
                return BadRequest(response);
            return Ok(response);
        }
    }
}