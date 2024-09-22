using Kraken.Aplicacao.ViewModel;

namespace Kraken.Aplicacao.Servicos.Contratos
{
    public interface IClienteServico
    {
        /// <summary>
        /// Adiciona um novo registro.
        /// </summary>
        /// <param name="model">view model a ser adicionado.</param>
        /// <returns>Um objeto ResultadoAPI contendo o resultado da operação.</returns>
        Task<ResultadoAPI<object>> AdicionarAsync(ClienteVM model);

        /// <summary>
        /// Remove um item existente.
        /// </summary>
        /// <param name="ID">O código identificador a ser removido.</param>
        /// <returns>Um objeto ResultadoAPI contendo o resultado da operação.</returns>
        Task<ResultadoAPI<object>> RemoverAsync(int id);

        /// <summary>
        /// Atualiza os dados de um item existente com base no código QR fornecido.
        /// </summary>
        /// <param name="model">O código QR do item a ser atualizado.</param>
        /// <returns>Um objeto ResultadoAPI contendo o resultado da operação.</returns>
        Task<ResultadoAPI<object>> EditarAsync(ClienteVM model);

        /// <summary>
        /// Busca todos os itens disponíveis.
        /// </summary>
        /// <returns>Um objeto ResultadoAPI contendo a lista de itens encontrados.</returns>
        Task<ResultadoAPI<object>> BuscarAsync();

        /// <summary>
        /// Obtém os detalhes de um cliente específico pelo ID.
        /// </summary>
        /// <param name="idCliente">O identificador único do cliente a ser obtido.</param>
        /// <returns>Um objeto ResultadoAPI contendo os detalhes do cliente.</returns>
        Task<ResultadoAPI<object>> ObterAsync(int idCliente);
    }
}