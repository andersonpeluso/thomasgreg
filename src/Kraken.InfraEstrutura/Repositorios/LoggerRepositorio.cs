using Dapper;
using Kraken.CrossCutting.Extensao;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.InfraEstrutura.Factory;

namespace Kraken.InfraEstrutura.Repositorios
{
    public sealed class LoggerRepositorio : ILoggerRepositorio
    {
        private readonly DbCredencial _dbCredencial;

        public LoggerRepositorio(DbCredencial dbCredencial)
        {
            _dbCredencial = dbCredencial;
        }

        public void Erro(string classe, string metodo, string mensagem = "")
        {
            var parametros = new DynamicParameters();
            parametros.Add("classe", classe);
            parametros.Add("metodo", metodo);
            parametros.Add("rotina", "ERRO");
            parametros.Add("mensagem", mensagem);
            parametros.Add("criado", DataHora.ObterHorarioBrasilia());

            _dbCredencial.Conexao.ExecuteScalar("SP_LOGGER_I", parametros);
        }

        public async Task ErroAsync(string classe, string metodo, string mensagem = "")
        {
            var parametros = new DynamicParameters();
            parametros.Add("classe", classe);
            parametros.Add("metodo", metodo);
            parametros.Add("rotina", "ERRO");
            parametros.Add("mensagem", mensagem);
            parametros.Add("criado", DataHora.ObterHorarioBrasilia());

            await _dbCredencial.Conexao.ExecuteScalarAsync("SP_LOGGER_I", parametros);
        }

        public async Task AvisoAsync(string classe, string metodo, string mensagem = "")
        {
            var parametros = new DynamicParameters();
            parametros.Add("classe", classe);
            parametros.Add("metodo", metodo);
            parametros.Add("rotina", "AVISO");
            parametros.Add("mensagem", mensagem);
            parametros.Add("criado", DataHora.ObterHorarioBrasilia());

            await _dbCredencial.Conexao.ExecuteScalarAsync("SP_LOGGER_I", parametros);
        }

        public void Aviso(string classe, string metodo, string mensagem = "")
        {
            var parametros = new DynamicParameters();
            parametros.Add("classe", classe);
            parametros.Add("metodo", metodo);
            parametros.Add("rotina", "AVISO");
            parametros.Add("mensagem", mensagem);
            parametros.Add("criado", DataHora.ObterHorarioBrasilia());

            _dbCredencial.Conexao.ExecuteScalar("SP_LOGGER_I", parametros);
        }
    }
}