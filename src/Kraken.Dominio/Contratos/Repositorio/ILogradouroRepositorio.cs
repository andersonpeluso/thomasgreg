using Kraken.Dominio.Entidades;

namespace Kraken.Dominio.Contratos.Repositorio
{
    public interface ILogradouroRepositorio : IBaseRepositorio<LogradouroDto>
    {
        /// <summary>
        /// Busca todos os logradouros associados a um determinado cliente no banco de dados.
        /// </summary>
        /// <param name="idCliente">O ID do cliente cujos logradouros devem ser buscados.</param>
        /// <returns>Retorna uma Task que resolve para uma lista de logradouros (LogradouroDto) do cliente especificado.</returns>
        Task<List<LogradouroDto>> BuscarPorClienteAsync(int idCliente);
    }
}