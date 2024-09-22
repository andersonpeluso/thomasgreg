using System.Net;
using System.Text.Json.Serialization;

namespace Kraken.Aplicacao.ViewModel
{
    public sealed class ResultadoAPI<T> where T : class
    {
        public HttpStatusCode Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Mensagem { get; set; } = string.Empty;

        public T Dados { get; set; }

        public ResultadoAPI(HttpStatusCode status, T dados = null, string? mensagem = null)
        {
            Status = status;
            Mensagem = mensagem;
            Dados = dados;
        }

        //public ResultadoAPI(HttpStatusCode status, T dados = null)
        //{
        //    Status = status;
        //    Dados = dados;
        //}

        //public ResultadoAPI(HttpStatusCode status, string? mensagem = null)
        //{
        //    Status = status;
        //    Mensagem = mensagem;
        //}

        //public ResultadoAPI(HttpStatusCode status)
        //{
        //    Status = status;
        //}
    }
}