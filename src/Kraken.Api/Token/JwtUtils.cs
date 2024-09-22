using Kraken.CrossCutting.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Kraken.API.Configuracao
{
    public interface IJwtUtils
    {
        public Task<int?> ValidarJsonWebToken(string token);
    }

    public class JwtUtils : IJwtUtils
    {
        private readonly ConfiguracaoDTO _configuracaoDTO;

        public JwtUtils(IOptions<ConfiguracaoDTO> configuracaoDTO)
        {
            _configuracaoDTO = configuracaoDTO.Value;
        }

        public async Task<int?> ValidarJsonWebToken(string token)
        {
            if (token is null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuracaoDTO.Secredo);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                //var teste1 = new IdentityRole(jwtToken.Claims.ToList()[2].ToString().Replace("perfil: ", ""));

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}