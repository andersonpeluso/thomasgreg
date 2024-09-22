using AspNetCoreRateLimit;
using Kraken.API.Configuracao;
using Kraken.Aplicacao.AutoMapper;
using Kraken.Aplicacao.Servicos;
using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.CrossCutting.DTO;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.InfraEstrutura.Repositorios;

namespace Kraken.Api.IoC
{
    public static class InjecaoDependencia
    {
        /// <summary>
        /// Injetar dependências.
        /// </summary>
        /// <param name="services">Serviços</param>
        /// <param name="configuration">Configuração</param>
        /// <returns></returns>
        public static IServiceCollection AdicionarServicos(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.Configure<ConfiguracaoDTO>(configuration.GetSection("ConfiguracaoToken"));

            // Service
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(typeof(AutoMapperConfiguracao));
            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<InfraEstrutura.Factory.DbCredencial>();
            services.AddScoped<IUnitOfWork, InfraEstrutura.Factory.UnitOfWork>();

            // ASPNET
            services.AddScoped<IClienteServico, ClienteServico>();
            services.AddScoped<ILogradouroServico, LogradouroServico>();

            // Repositorio
            services.AddScoped<ILoggerRepositorio, LoggerRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<ILogradouroRepositorio, LogradouroRepositorio>();

            //
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();

            return services;
        }

        /// <summary>
        /// Ler arquivos de configuração.
        /// </summary>
        /// <param name="services">Serviço</param>
        /// <param name="configuration">Configuração</param>
        /// <returns></returns>
        public static IServiceCollection LerArquivoConfiguracao(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            //services.Configure<ConfiguracaoSefazDTO>(configuration.GetSection("SintegraApi"));
            //services.Configure<ConexaoDTO>(configuration.GetSection("ConnectionStrings"));

            return services;
        }
    }
}