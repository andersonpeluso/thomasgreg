using System.ComponentModel.DataAnnotations;

namespace Kraken.Dominio.Entidades
{
    public abstract class BaseEntidade
    {
        [Key]
        public int? Id { get; set; }
    }
}