using AutoMapper;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Carteiras
{
    public class AtualizarCarteiraCommandHandler : IRequestHandler<AtualizarCarteiraCommand, CarteiraDto>
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public AtualizarCarteiraCommandHandler(ICarteiraRepository carteiraRepository, IMapper mapper)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<CarteiraDto> Handle(AtualizarCarteiraCommand request, CancellationToken cancellationToken)
        {
            var carteira = await _carteiraRepository.ObterPorIdAsync(request.Id);

            if (carteira == null)
            {
                // Ou lançar uma exceção específica de "NotFound"
                return null;
            }

            carteira.AtualizarNome(request.Nome);

            await _carteiraRepository.AtualizarAsync(carteira);

            return _mapper.Map<CarteiraDto>(carteira);
        }
    }
}