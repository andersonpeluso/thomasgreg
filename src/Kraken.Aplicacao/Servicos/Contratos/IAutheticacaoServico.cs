using Kraken.Aplicacao.ViewModel;

namespace Kraken.Aplicacao.Servicos.Contratos
{
    public interface IAutenticacaoServico
    {
        Task<ResultadoAPI<object>> AutenticarAsync(string login, string chave);
    }
}