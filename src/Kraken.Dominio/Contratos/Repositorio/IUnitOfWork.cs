namespace Kraken.Dominio.Contratos.Repositorio
{
    /// <summary>
    /// Classe responsável por gerenciar transações no contexto de uma unidade de trabalho (Unit of Work),
    /// utilizando a conexão do banco de dados fornecida pela classe DbCredencial.
    /// Implementa IUnitOfWork para suporte a transações atômicas.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Inicia uma transação no contexto atual de banco de dados de forma síncrona.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Inicia uma transação no contexto atual de banco de dados de forma assíncrona.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Realiza o commit da transação atual de forma síncrona, confirmando todas as operações.
        /// </summary>
        void Commit();

        /// <summary>
        /// Realiza o commit da transação atual de forma assíncrona, confirmando todas as operações.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        Task CommitAsync();

        /// <summary>
        /// Realiza o rollback da transação atual de forma síncrona, desfazendo todas as operações.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Realiza o rollback da transação atual de forma assíncrona, desfazendo todas as operações.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        Task RollbackAsync();
    }
}