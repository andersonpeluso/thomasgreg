using Dapper;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;
using Kraken.InfraEstrutura.Factory;

namespace Kraken.InfraEstrutura.Repositorios
{
    public sealed class LogradouroRepositorio : BaseRepositorio<LogradouroDto>, ILogradouroRepositorio
    {
        private readonly DbCredencial _dbCredencial;

        public LogradouroRepositorio(DbCredencial dbCredencial) : base(dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        public async Task<List<LogradouroDto>> BuscarPorClienteAsync(int idCliente)
        {
            // Busca os logradouros com base no IdCliente
            return (await _dbCredencial.Conexao.GetListAsync<LogradouroDto>(new { IdCliente = idCliente })).AsList();
        }
    }
}