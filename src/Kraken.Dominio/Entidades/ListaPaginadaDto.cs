using System.Text.Json.Serialization;

namespace Kraken.Dominio.Entidades
{
    public class ListaPaginadaDto<T> where T : class
    {
        public ListaPaginadaDto(int quantidadeRegistro, T dados, int paginaAtual, int pageSize)
        {
            TotalRegistros = quantidadeRegistro;
            Dados = dados;
            PaginaAtual = paginaAtual;
            TamanhoPagina = pageSize;
            TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / (double)TamanhoPagina);
            PaginaAnterior = PaginaAtual > 1;
            ProximaPagina = PaginaAtual < TotalPaginas;
        }

        [JsonPropertyName("totalRegistros")]
        public int TotalRegistros { get; set; }

        // tamanho da pagina
        [JsonPropertyName("quantidadePorPagina")]
        public int TamanhoPagina { get; set; }

        public int PaginaAtual { get; set; }

        [JsonPropertyName("totalPaginas")]
        public int TotalPaginas { get; set; }

        [JsonPropertyName("paginaAnterior")]
        public bool PaginaAnterior { get; set; }

        [JsonPropertyName("proximaPagina")]
        public bool ProximaPagina { get; set; }

        public T Dados { get; set; }
    }
}