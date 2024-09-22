using AutoMapper;
using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.Aplicacao.ViewModel;
using Kraken.CrossCutting.DTO;
using Kraken.Dominio.Contratos.Repositorio;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Kraken.Aplicacao.Servicos
{
    public class AutenticacaoServico : IAutenticacaoServico
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerRepositorio _loggerRepositorio;
        private readonly IAutenticacaoRepositorio _autenticacaoRepositorio;
        private readonly ConfiguracaoDTO _configuracaoDTO;

        public AutenticacaoServico(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   ILoggerRepositorio loggerRepositorio,
                                   IOptions<ConfiguracaoDTO> configuracaoDTO,
                                   IAutenticacaoRepositorio autenticacaoRepositorio)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerRepositorio = loggerRepositorio;
            _configuracaoDTO = configuracaoDTO.Value;
            _autenticacaoRepositorio = autenticacaoRepositorio;
        }

        public async Task<ResultadoAPI<object>> AutenticarAsync(string login, string chave)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var usuario = await _autenticacaoRepositorio.VerificarEmailExisteAsync(login.ToLower());

                if (usuario is not null && usuario.Status)
                {
                    if (usuario.Email == login && usuario.Senha == chave)
                        resultado = new ResultadoAPI<object>(HttpStatusCode.OK, await GerarJSONWebToken(usuario.Id.ToString()));
                    else
                        resultado = new ResultadoAPI<object>(HttpStatusCode.BadRequest, mensagem: "Usuário e senha não correspondem.");
                }
                else
                    resultado = new ResultadoAPI<object>(HttpStatusCode.NotFound, mensagem: "Usuário não encontrada!");
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(AutenticacaoServico), nameof(AutenticarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: ex.Message);
            }
            return resultado;
        }

        internal async Task<AutenticacaoVM> GerarJSONWebToken(string idUsuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, idUsuario)//,
                //new Claim(ClaimTypes.Role, perfil)
            };

            var secredo = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuracaoDTO.Secredo));

            var signingCredentials = new SigningCredentials(secredo, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims,

                                             expires: DateTime.UtcNow.AddHours((double)_configuracaoDTO.ExpiracaoHoras),
                                             signingCredentials: signingCredentials);

            var tokenCodificado = new JwtSecurityTokenHandler().WriteToken(token);

            var resultado = new AutenticacaoVM
            {
                Token = $"Bearer {tokenCodificado}",
                ExpiraEm = DateTime.UtcNow.AddHours(1)
            };

            return resultado;
        }
    }
}