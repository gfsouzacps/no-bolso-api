using MediatR;
using NoBolso.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Carteiras
{
    public class RemoverCarteiraCommandHandler : IRequestHandler<RemoverCarteiraCommand, bool>
    {
        private readonly ICarteiraRepository _carteiraRepository;

        public RemoverCarteiraCommandHandler(ICarteiraRepository carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }

        public async Task<bool> Handle(RemoverCarteiraCommand request, CancellationToken cancellationToken)
        {
            var carteiraExiste = await _carteiraRepository.ExisteAsync(request.Id);

            if (!carteiraExiste)
            {
                return false;
            }

            await _carteiraRepository.RemoverAsync(request.Id);
            return true;
        }
    }
}