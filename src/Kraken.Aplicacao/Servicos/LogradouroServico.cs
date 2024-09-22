using AutoMapper;
using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.Aplicacao.ViewModel;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;

namespace Kraken.Aplicacao.Servicos
{
    public class LogradouroServico : ILogradouroServico
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerRepositorio _loggerRepositorio;
        private readonly ILogradouroRepositorio _logradouroRepositorio;

        public LogradouroServico(IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 ILoggerRepositorio loggerRepositorio,
                                 ILogradouroRepositorio logradouroRepositorio)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerRepositorio = loggerRepositorio;
            _logradouroRepositorio = logradouroRepositorio;
        }

        public async Task<ResultadoAPI<object>> AdicionarAsync(LogradouroVM model)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var entidade = _mapper.Map<LogradouroDto>(model);

                entidade.Id = await _logradouroRepositorio.AdicionarAsync(entidade);

                if (entidade.Id.HasValue)
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.Created, model);
                else
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.NotFound, mensagem: Constantes.MensagemExecutaComandoGenerica);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(LogradouroServico), nameof(AdicionarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> BuscarAsync()
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var entidades = await _logradouroRepositorio.BuscarAsync();

                var models = _mapper.Map<List<LogradouroVM>>(entidades);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, models);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(LogradouroServico), nameof(BuscarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> EditarAsync(LogradouroVM model)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var entidade = _mapper.Map<LogradouroDto>(model);

                var atualizou = await _logradouroRepositorio.EditarAsync(entidade);

                if (atualizou)
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, model);
                else
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.NotFound, mensagem: Constantes.MensagemExecutaComandoGenerica);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(LogradouroServico), nameof(EditarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> ObterAsync(int idCliente)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var entidades = await _logradouroRepositorio.BuscarPorClienteAsync(idCliente);

                var models = _mapper.Map<List<LogradouroVM>>(entidades);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, models);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(LogradouroServico), nameof(BuscarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> RemoverAsync(int id)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var removeu = await _logradouroRepositorio.RemoverAsync(id);

                resultado = removeu
                    ? new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, Constantes.MensagemSucesso)
                    : new ResultadoAPI<object>(System.Net.HttpStatusCode.NotFound, mensagem: Constantes.MensagemExecutaComandoGenerica);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(LogradouroServico), nameof(RemoverAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }
    }
}