using Kraken.Aplicacao.Servicos.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kraken.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IAutenticacaoServico _autenticacaoServico;

        public OAuthController(IAutenticacaoServico autenticacaoServico)
        {
            _autenticacaoServico = autenticacaoServico;
        }

        /// <summary>
        /// Gera o token de acesso.
        /// </summary>
        /// <param name="usuario">Nome do usuário</param>
        /// <param name="chave">identificação única do usuário</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna token de acesso.</response>
        /// <response code="400">Retorna caso usuário e a chave não seja informado.</response>
        /// <response code="404">Retorna caso usuário e a chave sejam encontrado.</response>
        [AllowAnonymous]
        [HttpPost("autenticacao")]
        public async Task<ActionResult> Autenticar([FromHeader] string usuario, [FromHeader] string chave)
        {
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(chave))
            {
                var resultado = await _autenticacaoServico.AutenticarAsync(usuario, chave);

                return StatusCode((int)resultado.Status, string.IsNullOrEmpty(resultado.Mensagem) ? resultado.Dados : resultado.Mensagem);
            }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}