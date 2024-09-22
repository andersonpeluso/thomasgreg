using AutoMapper;
using Kraken.Aplicacao.ViewModel;
using Kraken.Dominio.Entidades;

namespace Kraken.Aplicacao.AutoMapper
{
    public class AutoMapperConfiguracao : Profile
    {
        public AutoMapperConfiguracao()
        {
            CreateMap<ClienteVM, ClienteDto>().ReverseMap();

            CreateMap<LogradouroVM, LogradouroDto>().ReverseMap();
        }
    }
}