using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.DTO;
using Tarefas.API.Interface;

namespace Tarefas.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class TarefaController(ITarefaAplication tarefaApp) : ControllerBase
    {
        private readonly ITarefaAplication _tarefaApp = tarefaApp;

        [Authorize(Roles = "Comum")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CriarTarefaRequestDTO dto)
        {
            var response = await _tarefaApp.CadastraTarefaAsync(dto);
            if (!response.Sucesso)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize(Roles = "Comum")]
        [HttpGet("{idTarefa}")]
        public async Task<ActionResult> GetId(int idTarefa)
        {
            var response = await _tarefaApp.ConsultarTarefaPorId(idTarefa);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize(Roles = "Comum")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] TarefaFiltroDTO dto)
        {
            var response = await _tarefaApp.FiltrarTarefasAsync(dto);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize(Roles = "Comum")]
        [HttpPatch("{idTarefa}")]
        public async Task<ActionResult> Patch(int idTarefa)
        {
            var response = await _tarefaApp.ExecutarTarefa(idTarefa);
            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }
    }
}