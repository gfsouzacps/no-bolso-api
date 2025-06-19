using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Application.Queries.Carteiras;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.Carteiras;

public class ObterCarteiraQueryHandler : IRequestHandler<ObterCarteiraQuery, ObterCarteiraQueryResult?>
{
    private readonly IRepository<Carteira> _carteiraRepository;

    public ObterCarteiraQueryHandler(IRepository<Carteira> carteiraRepository)
    {
        _carteiraRepository = carteiraRepository;
    }

    public async Task<ObterCarteiraQueryResult?> Handle(ObterCarteiraQuery request, CancellationToken cancellationToken)
    {
        var query = _carteiraRepository.GetQueryable();

        if (request.IncluirTransacoes)
        {
            query = query.Include(c => c.Transacoes);
        }

        var carteira = await query
            .Where(c => c.Id == request.Id)
            .Select(c => new ObterCarteiraQueryResult(
                c.Id,
                c.Nome,
                request.IncluirTransacoes
                    ? c.Transacoes.Select(t => new TransacaoResumidaResult(t.Id, t.Descricao, t.Valor)).ToList()
                    : new List<TransacaoResumidaResult>()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return carteira;
    }
}