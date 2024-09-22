using Kraken.Dominio.Contratos.Repositorio;

namespace Kraken.InfraEstrutura.Factory
{
    /// <summary>
    /// Classe responsável por gerenciar transações no contexto de uma unidade de trabalho (Unit of Work),
    /// utilizando a conexão do banco de dados fornecida pela classe DbCredencial.
    /// Implementa IUnitOfWork para suporte a transações atômicas.
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Instância da classe DbCredencial, que fornece a conexão e transação do banco de dados.
        /// </summary>
        private readonly DbCredencial _dbCredencial;

        /// <summary>
        /// Construtor da classe UnitOfWork. Recebe uma instância de DbCredencial para gerenciar as transações.
        /// </summary>
        /// <param name="dbCredencial">Instância de DbCredencial que contém a conexão com o banco de dados.</param>
        public UnitOfWork(DbCredencial dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        /// <summary>
        /// Inicia uma transação no contexto atual de banco de dados de forma síncrona.
        /// </summary>
        public void BeginTransaction()
        {
            _dbCredencial.Transacao = _dbCredencial.Conexao.BeginTransaction();
        }

        /// <summary>
        /// Inicia uma transação no contexto atual de banco de dados de forma assíncrona.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        public async Task BeginTransactionAsync()
        {
            _dbCredencial.Transacao = await _dbCredencial.Conexao.BeginTransactionAsync();
        }

        /// <summary>
        /// Realiza o commit da transação atual de forma síncrona, confirmando todas as operações.
        /// </summary>
        public void Commit()
        {
            _dbCredencial.Transacao?.Commit();
            Dispose();
        }

        /// <summary>
        /// Realiza o commit da transação atual de forma assíncrona, confirmando todas as operações.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        public async Task CommitAsync()
        {
            await _dbCredencial.Transacao?.CommitAsync();
            Dispose();
        }

        /// <summary>
        /// Realiza o rollback da transação atual de forma síncrona, desfazendo todas as operações.
        /// </summary>
        public void Rollback()
        {
            _dbCredencial.Transacao?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Realiza o rollback da transação atual de forma assíncrona, desfazendo todas as operações.
        /// </summary>
        /// <returns>Uma Task representando a operação assíncrona.</returns>
        public async Task RollbackAsync()
        {
            await _dbCredencial.Transacao?.RollbackAsync();
            Dispose();
        }

        /// <summary>
        /// Libera a transação atual, permitindo o gerenciamento correto do ciclo de vida da transação.
        /// </summary>
        public void Dispose()
        {
            _dbCredencial.Transacao = null;
        }
    }
}