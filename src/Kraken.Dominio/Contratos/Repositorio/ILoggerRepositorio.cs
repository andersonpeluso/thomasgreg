namespace Kraken.Dominio.Contratos.Repositorio
{
    /// <summary>
    /// Interface que define as operações para o repositório de logs. Fornece métodos para registrar
    /// logs de erros e avisos, tanto de forma síncrona quanto assíncrona.
    /// </summary>
    public interface ILoggerRepositorio
    {
        /// <summary>
        /// Registra um log de erro no sistema de forma síncrona.
        /// </summary>
        /// <param name="classe">O nome da classe onde o erro ocorreu.</param>
        /// <param name="metodo">O nome do método onde o erro ocorreu.</param>
        /// <param name="mensagem">Mensagem adicional sobre o erro. Opcional.</param>
        void Erro(string classe, string metodo, string mensagem = "");

        /// <summary>
        /// Registra um log de erro no sistema de forma assíncrona.
        /// </summary>
        /// <param name="classe">O nome da classe onde o erro ocorreu.</param>
        /// <param name="metodo">O nome do método onde o erro ocorreu.</param>
        /// <param name="mensagem">Mensagem adicional sobre o erro. Opcional.</param>
        /// <returns>Uma Task que representa a operação assíncrona de registro do erro.</returns>
        Task ErroAsync(string classe, string metodo, string mensagem = "");

        /// <summary>
        /// Registra um log de aviso no sistema de forma assíncrona.
        /// </summary>
        /// <param name="classe">O nome da classe onde o aviso foi gerado.</param>
        /// <param name="metodo">O nome do método onde o aviso foi gerado.</param>
        /// <param name="mensagem">Mensagem adicional sobre o aviso. Opcional.</param>
        /// <returns>Uma Task que representa a operação assíncrona de registro do aviso.</returns>
        Task AvisoAsync(string classe, string metodo, string mensagem = "");

        /// <summary>
        /// Registra um log de aviso no sistema de forma síncrona.
        /// </summary>
        /// <param name="classe">O nome da classe onde o aviso foi gerado.</param>
        /// <param name="metodo">O nome do método onde o aviso foi gerado.</param>
        /// <param name="mensagem">Mensagem adicional sobre o aviso. Opcional.</param>
        void Aviso(string classe, string metodo, string mensagem = "");
    }
}