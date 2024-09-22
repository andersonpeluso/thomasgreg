namespace Kraken.Aplicacao.ViewModel
{
    public sealed class LogradouroVM
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        /// <summary>
        /// Complemento (apto, bloco, etc.)
        /// </summary>
        public string Endereco { get; set; }

        /// <summary>
        /// Nome do bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        ///  Nome da cidade
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Sigla do estado
        /// </summary>
        public string Estado { get; set; }

        /// <summary>
        /// Complemento (apto, bloco, etc.)
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Código de Endereçamento Postal
        /// </summary>
        public string Cep { get; set; }
    }
}