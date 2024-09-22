using AutoMapper;
using Kraken.Aplicacao.Servicos.Contratos;
using Kraken.Aplicacao.ViewModel;
using Kraken.Dominio.Contratos.Repositorio;
using Kraken.Dominio.Entidades;

namespace Kraken.Aplicacao.Servicos
{
    public sealed class ClienteServico : IClienteServico
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerRepositorio _loggerRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ILogradouroRepositorio _logradouroRepositorio;

        public ClienteServico(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              ILoggerRepositorio loggerRepositorio,
                              IClienteRepositorio clienteRepositorio,
                              ILogradouroRepositorio logradouroRepositorio)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerRepositorio = loggerRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _logradouroRepositorio = logradouroRepositorio;
        }

        public async Task<ResultadoAPI<object>> AdicionarAsync(ClienteVM model)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                if (string.IsNullOrEmpty(model.Email))
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.NotAcceptable, mensagem: "Informe o e-mail.");
                else
                {
                    var emailExiste = await _clienteRepositorio.VerificarEmailExiste(model.Email);

                    if (!emailExiste)
                    {
                        var entidade = _mapper.Map<ClienteDto>(model);

                        await _unitOfWork.BeginTransactionAsync();

                        entidade.Id = await _clienteRepositorio.AdicionarAsync(entidade);

                        foreach (var endereco in entidade.Logradouro)
                        {
                            endereco.IdCliente = entidade.Id.Value;

                            endereco.Id = await _logradouroRepositorio.AdicionarAsync(endereco);
                        }
                        await _unitOfWork.CommitAsync();

                        resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.Created, model);
                    }
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();

                await _loggerRepositorio.ErroAsync(nameof(ClienteServico), nameof(AdicionarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> EditarAsync(ClienteVM model)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var entidade = _mapper.Map<ClienteDto>(model);

                var atualizou = await _clienteRepositorio.EditarAsync(entidade);

                if (atualizou)
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, model);
                else
                    resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.NotFound, mensagem: Constantes.MensagemExecutaComandoGenerica);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(ClienteServico), nameof(EditarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> BuscarAsync()
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var clientes = await _clienteRepositorio.BuscarAsync();

                if (clientes is not null)
                    foreach (var cliente in clientes)
                        cliente.Logradouro = await _logradouroRepositorio.BuscarPorClienteAsync(cliente.Id.Value);

                var models = _mapper.Map<List<ClienteVM>>(clientes);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, models);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(ClienteServico), nameof(BuscarAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> ObterAsync(int idCliente)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var cliente = await _clienteRepositorio.ObterPorIdAsync(idCliente);

                if (cliente is not null)
                    cliente.Logradouro = await _logradouroRepositorio.BuscarPorClienteAsync(cliente.Id.Value);

                var model = _mapper.Map<ClienteVM>(cliente);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, model);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(ClienteServico), nameof(ObterAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }

        public async Task<ResultadoAPI<object>> RemoverAsync(int id)
        {
            ResultadoAPI<object> resultado = null;

            try
            {
                var removeu = await _clienteRepositorio.RemoverAsync(id);

                resultado = removeu ? new ResultadoAPI<object>(System.Net.HttpStatusCode.OK, Constantes.MensagemSucesso)
                                    : new ResultadoAPI<object>(System.Net.HttpStatusCode.NotFound, mensagem: Constantes.MensagemExecutaComandoGenerica);
            }
            catch (Exception ex)
            {
                await _loggerRepositorio.ErroAsync(nameof(ClienteServico), nameof(RemoverAsync), ex.Message);

                resultado = new ResultadoAPI<object>(System.Net.HttpStatusCode.InternalServerError, mensagem: Constantes.MensagemErroGenerica);
            }
            return resultado;
        }
    }
}