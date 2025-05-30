using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Dtos;
using Sigma.Application.Interfaces;
using Sigma.Domain.Dtos;
using Sigma.Domain.Enums;
using Microsoft.AspNetCore.Authorization;


namespace Sigma.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController(IProjetoService projetoService)
        {
            _projetoService = projetoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("cadastro")]
        public async Task<IActionResult> Inserir([FromBody] ProjetoNovoDto model)
        {
            return new JsonResult(await _projetoService.Inserir(model));
        }

        [Authorize]
        [HttpGet]
        [Route("buscartodos")]

        public async Task<IActionResult> Buscar() {
            var projetos = await _projetoService.BuscarTodos();
            return Ok(projetos);
        }
        
        [Authorize]
        [HttpGet]
        [Route("buscarfiltrado")]
        public async Task<IActionResult> BuscarPorFiltro([FromQuery] string? nome, [FromQuery] StatusProjeto? status)
        {
            var projetos = await _projetoService.BuscarPorFiltro(nome, status);
            return Ok(projetos);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/deletar")]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
                await _projetoService.ExcluirProjetoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizaStatusDTo dto)
        {
            try
            {
                await _projetoService.AtualizarProjetoAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
