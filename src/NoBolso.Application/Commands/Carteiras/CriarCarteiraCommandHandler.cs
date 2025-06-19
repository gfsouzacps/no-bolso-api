using MediatR;
using NoBolso.Application.Commands.Carteiras;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Carteiras;

public class CriarCarteiraCommandHandler : IRequestHandler<CriarCarteiraCommand, Guid>
{
    private readonly IRepository<Carteira> _carteiraRepository;

    public CriarCarteiraCommandHandler(IRepository<Carteira> carteiraRepository)
    {
        _carteiraRepository = carteiraRepository;
    }

    public async Task<Guid> Handle(CriarCarteiraCommand request, CancellationToken cancellationToken)
    {
        var carteira = new Carteira(request.Nome, request.usuarioId);

        await _carteiraRepository.AddAsync(carteira, cancellationToken);
        await _carteiraRepository.SaveChangesAsync(cancellationToken);

        return carteira.Id;
    }
}