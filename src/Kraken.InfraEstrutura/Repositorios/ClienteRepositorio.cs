using Dapper;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;
using Kraken.InfraEstrutura.Factory;

namespace Kraken.InfraEstrutura.Repositorios
{
    public sealed class ClienteRepositorio : BaseRepositorio<ClienteDto>, IClienteRepositorio
    {
        private readonly DbCredencial _dbCredencial;

        public ClienteRepositorio(DbCredencial dbCredencial) : base(dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        public async Task<bool> VerificarEmailExiste(string email)
        {
            // Consulta para verificar se o e-mail existe
            var cliente = (await _dbCredencial.Conexao.GetListAsync<ClienteDto>(new { Email = email })).AsList().FirstOrDefault();

            // Retorna true se o cliente for encontrado, false se não
            return cliente != null;
        }
    }
}