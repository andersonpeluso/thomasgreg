using System.ComponentModel.DataAnnotations.Schema;

namespace Kraken.Dominio.Entidades
{
    [Table("Clientes")]
    public sealed class ClienteDto : BaseEntidade
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Endereço de e-mail do cliente. Deve ser único.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Logotipo do cliente em formato Base64.
        /// </summary>
        public string Logotipo { get; set; }

        /// <summary>
        /// Logotipo do cliente em formato Base64.
        /// </summary>
        public List<LogradouroDto> Logradouro { get; set; } = new List<LogradouroDto>();
    }
}