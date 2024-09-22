using Kraken.Dominio.Entidades;

namespace Kraken.Dominio.Contratos.Repositorio
{
    public interface IClienteRepositorio : IBaseRepositorio<ClienteDto>
    {
        /// <summary>
        /// Verifica se um cliente com o e-mail especificado já existe no banco de dados.
        /// </summary>
        /// <param name="email">O e-mail do cliente a ser verificado.</param>
        /// <returns>
        /// Retorna uma Task que resolve em true se o e-mail já estiver cadastrado, ou false se o e-mail estiver disponível.
        /// </returns>
        Task<bool> VerificarEmailExiste(string email);
    }
}