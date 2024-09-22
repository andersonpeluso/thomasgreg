using System.ComponentModel.DataAnnotations.Schema;

namespace Kraken.Dominio.Entidades
{
    [Table("Usuarios")]
    public sealed class AutenticacaoDto : BaseEntidade
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}