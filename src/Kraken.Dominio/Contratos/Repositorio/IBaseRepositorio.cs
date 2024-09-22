using Kraken.Dominio.Entidades;
using System.Text;

namespace Kraken.Dominio.Contratos.Repositorio
{
    public interface IBaseRepositorio<TEntity> where TEntity : BaseEntidade
    {
        /// <summary>
        /// Adiciona uma nova entidade no banco de dados.
        /// </summary>
        /// <param name="entidade">A entidade a ser adicionada.</param>
        /// <returns>Retorna o ID da entidade recém-adicionada ou null se a operação falhar.</returns>
        Task<int?> AdicionarAsync(TEntity entidade);

        /// <summary>
        /// Atualiza uma entidade existente no banco de dados.
        /// </summary>
        /// <param name="entidade">A entidade a ser atualizada.</param>
        /// <returns>Retorna true se a atualização for bem-sucedida; caso contrário, false.</returns>
        Task<bool> EditarAsync(TEntity entidade);

        /// <summary>
        /// Busca todas as entidades do banco de dados.
        /// </summary>
        /// <returns>Retorna uma lista de todas as entidades encontradas.</returns>
        Task<List<TEntity>> BuscarAsync();

        /// <summary>
        /// Busca entidades com base em uma condição opcional.
        /// </summary>
        /// <param name="condicional">Uma condição em formato de string para filtrar as entidades.</param>
        /// <returns>Retorna uma lista de entidades que atendem à condição, ou todas se nenhuma condição for passada.</returns>
        Task<List<TEntity>> BuscarAsync(string condicional = null);

        /// <summary>
        /// Obtém uma entidade pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da entidade a ser buscada.</param>
        /// <returns>Retorna a entidade correspondente ao ID, ou null se não for encontrada.</returns>
        Task<TEntity> ObterPorIdAsync(int id);

        /// <summary>
        /// Obtém uma entidade com base em uma condição opcional.
        /// </summary>
        /// <param name="condicional">Uma condição em formato de string para filtrar a entidade.</param>
        /// <returns>Retorna a entidade correspondente à condição ou null se não for encontrada.</returns>
        Task<TEntity> ObterAsync(string condicional = null);

        /// <summary>
        /// Busca entidades de forma paginada, com uma condição e ordenação opcionais.
        /// </summary>
        /// <param name="pagina">O número da página atual.</param>
        /// <param name="registroPorPagina">O número de registros por página.</param>
        /// <param name="condicional">Uma condição em formato de string para filtrar as entidades.</param>
        /// <param name="ordernarPor">Campo de ordenação opcional.</param>
        /// <returns>Retorna uma lista de entidades paginada com base nos parâmetros fornecidos.</returns>
        Task<List<TEntity>> BuscarPaginadaAsync(int pagina, int registroPorPagina, string condicional = null, string ordernarPor = null);

        /// <summary>
        /// Busca entidades paginadas com base em uma consulta customizada.
        /// </summary>
        /// <typeparam name="T">O tipo da entidade.</typeparam>
        /// <param name="consulta">Um StringBuilder contendo a consulta SQL personalizada.</param>
        /// <param name="numeroPagina">Número da página a ser buscada. Padrão é 1.</param>
        /// <param name="registroPorPagina">Número de registros por página. Padrão é 50.</param>
        /// <returns>Retorna um DTO contendo a lista de entidades paginada e informações de paginação.</returns>
        Task<ListaPaginadaDto<List<T>>> BuscarPaginadaAsync<T>(StringBuilder consulta, int numeroPagina = 1, int registroPorPagina = 50);

        /// <summary>
        /// Remove uma entidade do banco de dados.
        /// </summary>
        /// <param name="entidade">A entidade a ser removida.</param>
        /// <returns>Retorna true se a remoção for bem-sucedida; caso contrário, false.</returns>
        Task<bool> RemoverAsync(TEntity entidade);

        /// <summary>
        /// Remove uma entidade com base no seu ID.
        /// </summary>
        /// <param name="id">O ID da entidade a ser removida.</param>
        /// <returns>Retorna true se a remoção for bem-sucedida; caso contrário, false.</returns>
        Task<bool> RemoverAsync(int id);

        /// <summary>
        /// Obtém a quantidade de registros no banco de dados com base em uma condição opcional.
        /// </summary>
        /// <param name="condicional">Uma condição em formato de string para filtrar os registros.</param>
        /// <returns>Retorna o número de registros que atendem à condição.</returns>
        Task<int> QuantidadeRegistrosAsync(string condicional);
    }
}