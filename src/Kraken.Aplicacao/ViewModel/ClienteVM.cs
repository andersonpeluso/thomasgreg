namespace Kraken.Aplicacao.ViewModel
{
    public class ClienteVM
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Logotipo { get; set; }

        public List<LogradouroVM> Logradouro { get; set; } = new List<LogradouroVM>();
    }
}