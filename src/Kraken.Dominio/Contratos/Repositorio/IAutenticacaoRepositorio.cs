using Kraken.Dominio.Entidades;

namespace Kraken.Dominio.Contratos.Repositorio
{
    public interface IAutenticacaoRepositorio
    {
        Task<AutenticacaoDto> VerificarEmailExisteAsync(string email);
    }
}