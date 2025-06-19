using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Application.Queries.Carteiras;
using NoBolso.Domain.Interfaces.Repositories;

namespace NoBolso.Application.Queries.Carteiras
{
    public class ObterCarteiraQueryHandler : IRequestHandler<ObterCarteiraQuery, CarteiraDto>
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public ObterCarteiraQueryHandler(ICarteiraRepository carteiraRepository, IMapper mapper)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<CarteiraDto> Handle(ObterCarteiraQuery request, CancellationToken cancellationToken)
        {
            var carteira = request.IncluirTransacoes
                ? await _carteiraRepository.ObterPorIdComTransacoesAsync(request.Id)
                : await _carteiraRepository.ObterPorIdAsync(request.Id);

            if (carteira == null)
                throw new InvalidOperationException($"Carteira com ID {request.Id} não foi encontrada.");

            return _mapper.Map<CarteiraDto>(carteira);
        }
    }
}