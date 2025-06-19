using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NoBolso.Application.Commands.Carteiras;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces.Repositories;

namespace NoBolso.Application.Commands.Carteiras
{
    public class CriarCarteiraCommandHandler : IRequestHandler<CriarCarteiraCommand, CarteiraDto>
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public CriarCarteiraCommandHandler(ICarteiraRepository carteiraRepository, IMapper mapper)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<CarteiraDto> Handle(CriarCarteiraCommand request, CancellationToken cancellationToken)
        {
            var carteira = new Carteira(request.Nome);
            var carteiraSalva = await _carteiraRepository.AdicionarAsync(carteira);

            return _mapper.Map<CarteiraDto>(carteiraSalva);
        }
    }
}