using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.DTO;
using Tarefas.API.Interface;

namespace Tarefas.API.Controller
{
    [ApiController]
    [Route("api/v1/{controller}")]

    public class TarefaController(ITarefaAplication tarefaApp) : ControllerBase
    {
        private readonly ITarefaAplication _tarefaApp = tarefaApp;

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CriarTarefaRequestDTO dto)
        {
            var response = _tarefaApp.CadastraTarefaAsync(dto);
            if (response != null)
                return BadRequest();
            return Ok(response);
        }

        [HttpGet("{idTarefa}")]
        public async Task<ActionResult> GetId(int idTarefa)
        {
            var response = _tarefaApp.ConsultarTarefaPorId(idTarefa);
            if (response != null)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] TarefaFiltroDTO dto)
        {
            var response = _tarefaApp.FiltrarTarefasAsync(dto);
            if (response != null)
                return BadRequest();

            return Ok(response);
        }

        [HttpPatch("{idTarefa}")]
        public async Task<ActionResult> patch(int idTarefa)
        {
            var response = _tarefaApp.ExecutarTarefa(idTarefa);
            if (response != null)
                return BadRequest();

            return Ok(response);
        }
    }
}