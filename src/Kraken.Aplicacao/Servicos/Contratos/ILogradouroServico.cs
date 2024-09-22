using Kraken.Aplicacao.ViewModel;

namespace Kraken.Aplicacao.Servicos.Contratos
{
    /// <summary>
    /// Interface que define as operações relacionadas ao serviço de logradouros.
    /// Fornece métodos para adicionar, remover, editar e buscar logradouros.
    /// </summary>
    public interface ILogradouroServico
    {
        /// <summary>
        /// Adiciona um novo logradouro de forma assíncrona.
        /// </summary>
        /// <param name="model">O modelo de logradouro a ser adicionado.</param>
        /// <returns>Retorna um objeto ResultadoAPI com o resultado da operação.</returns>
        Task<ResultadoAPI<object>> AdicionarAsync(LogradouroVM model);

        /// <summary>
        /// Remove um logradouro pelo seu ID de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do logradouro a ser removido.</param>
        /// <returns>Retorna um objeto ResultadoAPI com o resultado da operação.</returns>
        Task<ResultadoAPI<object>> RemoverAsync(int id);

        /// <summary>
        /// Edita as informações de um logradouro existente de forma assíncrona.
        /// </summary>
        /// <param name="model">O modelo de logradouro com as informações atualizadas.</param>
        /// <returns>Retorna um objeto ResultadoAPI com o resultado da operação.</returns>
        Task<ResultadoAPI<object>> EditarAsync(LogradouroVM model);

        /// <summary>
        /// Busca todos os logradouros de forma assíncrona.
        /// </summary>
        /// <returns>Retorna um objeto ResultadoAPI contendo a lista de logradouros.</returns>
        Task<ResultadoAPI<object>> BuscarAsync();

        /// <summary>
        /// Obtém todos os logradouros associados a um cliente específico de forma assíncrona.
        /// </summary>
        /// <param name="idCliente">O ID do cliente cujos logradouros devem ser obtidos.</param>
        /// <returns>Retorna um objeto ResultadoAPI contendo os logradouros do cliente especificado.</returns>
        Task<ResultadoAPI<object>> ObterAsync(int idCliente);
    }
}