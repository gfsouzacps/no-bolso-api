using AutoMapper;
using MediatR;
using NoBolso.Application.DTOs;
using NoBolso.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.Carteiras
{
    public class ListarCarteirasQueryHandler : IRequestHandler<ListarCarteirasQuery, IEnumerable<CarteiraDto>>
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public ListarCarteirasQueryHandler(ICarteiraRepository carteiraRepository, IMapper mapper)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarteiraDto>> Handle(ListarCarteirasQuery request, CancellationToken cancellationToken)
        {
            var carteiras = await _carteiraRepository.ObterTodasAsync();
            return _mapper.Map<IEnumerable<CarteiraDto>>(carteiras);
        }
    }
}