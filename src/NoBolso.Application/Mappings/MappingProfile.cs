using AutoMapper;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Entities;
using NoBolso.Application.Commands.Transacoes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NoBolso.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transacao, TransacaoDto>()
                .ForMember(dest => dest.NomeCarteira, opt => opt.Ignore()); // Será preenchido manualmente

            CreateMap<Carteira, CarteiraDto>()
                .ForMember(dest => dest.QuantidadeTransacoes, opt => opt.MapFrom(src => src.Transacoes.Count))
                .ForMember(dest => dest.Transacoes, opt => opt.MapFrom(src => src.Transacoes));
        }
    }
}