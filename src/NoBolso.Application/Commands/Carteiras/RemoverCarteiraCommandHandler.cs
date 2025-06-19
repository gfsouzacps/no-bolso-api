using MediatR;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Carteiras;

public class RemoverCarteiraCommandHandler : IRequestHandler<RemoverCarteiraCommand, Unit>
{
    private readonly IRepository<Carteira> _carteiraRepository;

    public RemoverCarteiraCommandHandler(IRepository<Carteira> carteiraRepository)
    {
        _carteiraRepository = carteiraRepository;
    }

    public async Task<Unit> Handle(RemoverCarteiraCommand request, CancellationToken cancellationToken)
    {
        var carteira = await _carteiraRepository.GetByIdAsync(request.Id, cancellationToken);

        if (carteira == null)
        {
            throw new Exception($"Carteira com ID {request.Id} não encontrada.");
        }

        _carteiraRepository.Delete(carteira);
        await _carteiraRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}