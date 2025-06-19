using MediatR;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Commands.Carteiras;

public class AtualizarCarteiraCommandHandler : IRequestHandler<AtualizarCarteiraCommand, Unit>
{
    private readonly IRepository<Carteira> _carteiraRepository;

    public AtualizarCarteiraCommandHandler(IRepository<Carteira> carteiraRepository)
    {
        _carteiraRepository = carteiraRepository;
    }

    public async Task<Unit> Handle(AtualizarCarteiraCommand request, CancellationToken cancellationToken)
    {
        var carteira = await _carteiraRepository.GetByIdAsync(request.Id, cancellationToken);

        if (carteira == null)
        {
            throw new Exception($"Carteira com ID {request.Id} não encontrada.");
        }

        carteira.AtualizarNome(request.Nome);

        await _carteiraRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}