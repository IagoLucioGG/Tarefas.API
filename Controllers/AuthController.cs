using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.DTO;

namespace Tarefas.API.Controller
{
    [ApiController]
    [Route("auth/{controller}")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthAplication _authApp;
        public LoginController(IAuthAplication authApp)
        {
            _authApp = authApp;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginRequestDTO dto)
        {
            var response = _authApp.LoginAsync(dto);
            return Ok(response);
        }
    }
}