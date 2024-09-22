using Dapper;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;
using Kraken.InfraEstrutura.Factory;

namespace Kraken.InfraEstrutura.Repositorios
{
    public sealed class AutenticacaoRepositorio : IAutenticacaoRepositorio
    {
        private readonly DbCredencial _dbCredencial;

        public AutenticacaoRepositorio(DbCredencial dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        public async Task<AutenticacaoDto> VerificarEmailExisteAsync(string email)
        {
            // Consulta para verificar se o e-mail existe
            return await _dbCredencial.Conexao.GetAsync<AutenticacaoDto>(new { Email = email });
        }
    }
}