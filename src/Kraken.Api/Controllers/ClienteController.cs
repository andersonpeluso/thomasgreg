using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.Aplicacao.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kraken.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;

        public ClienteController(IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        /// <summary>
        /// Inserir um novo registro no sistema.
        /// </summary>
        /// <param name="model">View Model</param>
        /// <returns>ActionResult</returns>
        /// <response code="201">Objeto criado.</response>
        /// <response code="406">Caso não seja informado o e-mail.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 406)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> AdicionarAsync(ClienteVM model)
        {
            var resultado = await _clienteServico.AdicionarAsync(model);

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
        public async Task<ActionResult> EditarAsync(ClienteVM model)
        {
            var resultado = await _clienteServico.EditarAsync(model);

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
            var resultado = await _clienteServico.BuscarAsync();

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }

        /// <summary>
        /// Recuperar um único registro no sistema usando o ID.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado esparado.</response>
        /// <response code="500">Retorna para erro interno.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 200)]
        [ProducesResponseType(typeof(ResultadoAPI<object>), 500)]
        public async Task<ActionResult> BuscarAsync(int id)
        {
            var resultado = await _clienteServico.ObterAsync(id);

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
            var resultado = await _clienteServico.RemoverAsync(id);

            return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
        }
    }
}