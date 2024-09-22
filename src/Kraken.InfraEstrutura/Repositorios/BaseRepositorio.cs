using Dapper;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;
using Kraken.InfraEstrutura.Factory;
using System.Text;

namespace Kraken.InfraEstrutura.Repositorios
{
    /// <summary>
    /// Classe abstrata que implementa a lógica básica de um repositório genérico para entidades que derivam de BaseEntidade.
    /// Fornece métodos para realizar operações de CRUD (Create, Read, Update, Delete) e busca paginada.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade que será gerenciada pelo repositório.</typeparam>
    public abstract class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : BaseEntidade
    {
        /// <summary>
        /// Instância de DbCredencial que contém a conexão com o banco de dados e a transação atual.
        /// </summary>
        private readonly DbCredencial _dbCredencial;

        /// <summary>
        /// Construtor que inicializa o repositório com a credencial de banco de dados.
        /// </summary>
        /// <param name="dbCredencial">A credencial de banco de dados utilizada para operações de conexão e transação.</param>
        protected BaseRepositorio(DbCredencial dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        /// <summary>
        /// Adiciona uma nova entidade ao banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entidade">A entidade que será adicionada.</param>
        /// <returns>O ID da entidade recém-adicionada, ou null se a inserção falhar.</returns>
        public virtual async Task<int?> AdicionarAsync(TEntity entidade)
        {
            return await _dbCredencial.Conexao.InsertAsync(entidade, _dbCredencial.Transacao, _dbCredencial.Conexao.ConnectionTimeout);
        }

        /// <summary>
        /// Atualiza uma entidade existente no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entidade">A entidade a ser atualizada.</param>
        /// <returns>True se a atualização for bem-sucedida; caso contrário, false.</returns>
        public virtual async Task<bool> EditarAsync(TEntity entidade)
        {
            return await _dbCredencial.Conexao.UpdateAsync(entidade, _dbCredencial.Transacao, _dbCredencial.Conexao.ConnectionTimeout) == 1;
        }

        /// <summary>
        /// Busca todas as entidades do tipo TEntity no banco de dados de forma assíncrona.
        /// </summary>
        /// <returns>Uma lista de entidades encontradas.</returns>
        public virtual async Task<List<TEntity>> BuscarAsync()
        {
            return (await _dbCredencial.Conexao.GetListAsync<TEntity>()).AsList();
        }

        /// <summary>
        /// [Obsoleto] Busca entidades com base em uma condição fornecida de forma assíncrona.
        /// </summary>
        /// <param name="condicional">Uma condição SQL para filtrar as entidades.</param>
        /// <returns>Uma lista de entidades que atendem à condição.</returns>
        [Obsolete("Fazer análise", true)]
        public virtual async Task<List<TEntity>> BuscarAsync(string condicional)
        {
            return (await _dbCredencial.Conexao.GetListAsync<TEntity>(condicional,
                                                                      _dbCredencial.Transacao,
                                                                      _dbCredencial.Conexao.ConnectionTimeout)).AsList();
        }

        /// <summary>
        /// Obtém uma entidade específica pelo seu ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID da entidade a ser obtida.</param>
        /// <returns>A entidade correspondente ao ID fornecido, ou null se não for encontrada.</returns>
        public virtual async Task<TEntity> ObterPorIdAsync(int id)
        {
            return await _dbCredencial.Conexao.GetAsync<TEntity>(id,
                                                                 _dbCredencial.Transacao,
                                                                 _dbCredencial.Conexao.ConnectionTimeout);
        }

        /// <summary>
        /// Obtém a primeira entidade que atende a uma condição de forma assíncrona.
        /// </summary>
        /// <param name="condicional">Uma condição SQL opcional para filtrar a entidade.</param>
        /// <returns>A primeira entidade que atende à condição, ou null se não for encontrada.</returns>
        public virtual async Task<TEntity> ObterAsync(string condicional = null)
        {
            var resultado = (await _dbCredencial.Conexao.GetListAsync<TEntity>(condicional,
                                                                              transaction: _dbCredencial.Transacao,
                                                                              _dbCredencial.Conexao.ConnectionTimeout)).AsList();

            return resultado[0];
        }

        /// <summary>
        /// Busca uma lista paginada de entidades com base em parâmetros de paginação e uma condição opcional.
        /// </summary>
        /// <param name="pagina">O número da página atual.</param>
        /// <param name="registroPorPagina">O número de registros por página.</param>
        /// <param name="condicional">Uma condição SQL opcional para filtrar as entidades.</param>
        /// <param name="ordernarPor">Campo pelo qual as entidades devem ser ordenadas.</param>
        /// <returns>Uma lista paginada de entidades.</returns>
        public virtual async Task<List<TEntity>> BuscarPaginadaAsync(int pagina,
                                                                     int registroPorPagina,
                                                                     string condicional = null,
                                                                     string ordernarPor = null)
        {
            return (await _dbCredencial.Conexao.GetListPagedAsync<TEntity>(pagina,
                                                                           registroPorPagina,
                                                                           condicional,
                                                                           ordernarPor,
                                                                           transaction: _dbCredencial.Transacao)).AsList();
        }

        /// <summary>
        /// Busca uma lista paginada de entidades com base em uma consulta SQL customizada.
        /// </summary>
        /// <typeparam name="T">O tipo de dados retornado pela consulta.</typeparam>
        /// <param name="consulta">Um StringBuilder contendo a consulta SQL.</param>
        /// <param name="numeroPagina">O número da página atual. Padrão é 1.</param>
        /// <param name="registroPorPagina">O número de registros por página. Padrão é 50.</param>
        /// <returns>Um DTO contendo a lista paginada de entidades e informações de paginação.</returns>
        public virtual async Task<ListaPaginadaDto<List<T>>> BuscarPaginadaAsync<T>(StringBuilder consulta,
                                                                                    int numeroPagina = 1,
                                                                                    int registroPorPagina = 50)
        {
            consulta.AppendLine("OFFSET @QuantidadeRegistroPorPagina ROWS FETCH NEXT @QuantidadePagina ROWS ONLY");

            var quantidadeRegistroPorPagina = (numeroPagina - 1) * registroPorPagina;
            var quantidadePagina = registroPorPagina;

            var reader = await _dbCredencial.Conexao.QueryMultipleAsync(consulta.ToString(),
                                                                        new { QuantidadeRegistroPorPagina = quantidadeRegistroPorPagina, QuantidadePagina = quantidadePagina },
                                                                        _dbCredencial.Transacao,
                                                                        _dbCredencial.Conexao.ConnectionTimeout);

            var totalRegistro = (await reader.ReadAsync<int>()).FirstOrDefault();

            var dados = (await reader.ReadAsync<T>()).AsList();

            return new ListaPaginadaDto<List<T>>(totalRegistro, dados, numeroPagina, registroPorPagina);
        }

        /// <summary>
        /// Remove uma entidade do banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entidade">A entidade a ser removida.</param>
        /// <returns>True se a remoção for bem-sucedida; caso contrário, false.</returns>
        public virtual async Task<bool> RemoverAsync(TEntity entidade)
        {
            return await _dbCredencial.Conexao.DeleteAsync(entidade, _dbCredencial.Transacao) == 1;
        }

        /// <summary>
        /// Remove uma entidade pelo seu ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID da entidade a ser removida.</param>
        /// <returns>True se a remoção for bem-sucedida; caso contrário, false.</returns>
        public virtual async Task<bool> RemoverAsync(int id)
        {
            return await _dbCredencial.Conexao.DeleteAsync<TEntity>(id, _dbCredencial.Transacao) == 1;
        }

        /// <summary>
        /// Retorna a quantidade de registros que atendem a uma condição específica.
        /// </summary>
        /// <param name="condicional">Uma condição SQL para filtrar os registros.</param>
        /// <returns>O número de registros que atendem à condição.</returns>
        public virtual async Task<int> QuantidadeRegistrosAsync(string condicional)
        {
            return await _dbCredencial.Conexao.RecordCountAsync<int>(condicional, transaction: _dbCredencial.Transacao);
        }
    }
}