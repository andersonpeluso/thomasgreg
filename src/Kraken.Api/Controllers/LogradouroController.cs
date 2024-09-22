using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.Aplicacao.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kraken.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogradouroController : ControllerBase
    {
        private readonly ILogradouroServico _logradouroServico;

        public LogradouroController(ILogradouroServico logradouroServico)
        {
            _logradouroServico = logradouroServico;
        }

        /// <summary>
        /// Inserir um novo registro no sistema.
        /// </summary>
        /// <param name="model">View Model</param>
        /// <returns>ActionResult</returns>
        /// <response code="201">Objeto criado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> AdicionarAsync(LogradouroVM model)
        {
            var resultado = await _logradouroServico.AdicionarAsync(model);

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }

        /// <summary>
        /// Modificar ou ajustar um registro existente no sistema.
        /// </summary>
        /// <param name="model">View Model</param>
        /// <returns></returns>
        /// <response code="200">Objeto editado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpPut]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> EditarAsync(LogradouroVM model)
        {
            var resultado = await _logradouroServico.EditarAsync(model);

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }

        /// <summary>
        /// Recuperar todos os registros armazenados no sistema.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado esparado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> BuscarAsync()
        {
            var resultado = await _logradouroServico.BuscarAsync();

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }

        /// <summary>
        /// Recuperar um único registro no sistema usando o ID.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado esparado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpGet("{idCliente:int}")]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> BuscarAsync(int idCliente)
        {
            var resultado = await _logradouroServico.ObterAsync(idCliente);

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }

        /// <summary>
        /// Excluir permanentemente um registro do sistema.
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        /// <response code="200">Resultado esparado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> RemoverAsync(int id)
        {
            var resultado = await _logradouroServico.RemoverAsync(id);

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }
    }
}