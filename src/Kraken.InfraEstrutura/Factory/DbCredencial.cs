using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Kraken.InfraEstrutura.Factory
{
    /// <summary>
    /// Classe responsável por gerenciar a conexão com o banco de dados, utilizando credenciais
    /// armazenadas no arquivo de configuração. Implementa a interface IDisposable para garantir o
    /// descarte adequado da conexão.
    /// </summary>
    public sealed class DbCredencial : IDisposable
    {
        /// <summary>
        /// Configurações da aplicação, carregadas a partir de um arquivo de configuração.
        /// </summary>
        public IConfiguration _configuration;

        /// <summary>
        /// Identificador único para a instância da classe.
        /// </summary>
        private Guid Id;

        /// <summary>
        /// Objeto de conexão com o banco de dados.
        /// </summary>
        public DbConnection Conexao { get; }

        /// <summary>
        /// Transação atual do banco de dados.
        /// </summary>
        public DbTransaction Transacao { get; set; }

        /// <summary>
        /// Construtor da classe DbCredencial. Inicializa a conexão com o banco de dados utilizando
        /// as credenciais fornecidas pela configuração.
        /// </summary>
        /// <param name="configuration">As configurações da aplicação (geralmente carregadas de appsettings.json).</param>
        public DbCredencial(IConfiguration configuration)
        {
            _configuration = configuration;
            Id = Guid.NewGuid();
            Conexao = new SqlConnection(ObterDadosConexao());
            Conexao.Open();
        }

        /// <summary>
        /// Obtém as informações de conexão com o banco de dados a partir do arquivo de configuração.
        /// </summary>
        /// <returns>Uma string contendo a string de conexão.</returns>
        internal string? ObterDadosConexao()
        {
            var configuracao = LerConfiguracao();

            var NomeServidor = configuracao.GetConnectionString("Servidor").Replace(" ", "").ToUpper();
            var Banco = configuracao.GetConnectionString("Banco").Replace(" ", "").ToUpper();
            var Usuario = configuracao.GetConnectionString("Usuario").Replace(" ", "").ToUpper();
            var Senha = configuracao.GetConnectionString("Senha").Replace(" ", "");
            var timeOut = configuracao.GetConnectionString("Timeout").Replace(" ", "");

            var stringBuilder = new System.Text.StringBuilder(500);
            stringBuilder.Append($"Server={NomeServidor};");
            stringBuilder.Append($"Initial Catalog={Banco};");
            stringBuilder.Append("Persist Security Info=False;");
            stringBuilder.Append($"User ID={Usuario};");
            stringBuilder.Append($"Password={Senha};");
            stringBuilder.Append("MultipleActiveResultSets=False;");
            stringBuilder.Append("Encrypt=True;");
            stringBuilder.Append("TrustServerCertificate=true;");
            stringBuilder.Append($"Connection Timeout={timeOut};");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Carrega as configurações do arquivo JSON apropriado, de acordo com o ambiente de execução (ex: Desenvolvimento ou Produção).
        /// </summary>
        /// <returns>As configurações do aplicativo como um objeto IConfigurationRoot.</returns>
        internal IConfigurationRoot LerConfiguracao()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var tipoDepuracao = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var path = Path.Combine(Directory.GetCurrentDirectory(), !string.IsNullOrEmpty(tipoDepuracao) ? $"appsettings.{tipoDepuracao}.json" : "appsettings.json");

            configurationBuilder.AddJsonFile(path, false);

            return configurationBuilder.Build();
        }

        /// <summary>
        /// Descarrega e libera a conexão com o banco de dados.
        /// </summary>
        public void Dispose()
        {
            Conexao?.Dispose();
        }
    }
}