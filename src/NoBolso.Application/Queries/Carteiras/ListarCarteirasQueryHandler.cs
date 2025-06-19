using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;
using NoBolso.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.Carteiras;

public class ListarCarteirasQueryHandler : IRequestHandler<ListarCarteirasQuery, List<ListarCarteirasQueryResult>>
{
    private readonly IRepository<Carteira> _carteiraRepository;

    public ListarCarteirasQueryHandler(IRepository<Carteira> carteiraRepository)
    {
        _carteiraRepository = carteiraRepository;
    }

    public async Task<List<ListarCarteirasQueryResult>> Handle(ListarCarteirasQuery request, CancellationToken cancellationToken)
    {
        return await _carteiraRepository
            .GetQueryable()
            .Where(c => c.GrupoId == request.GrupoId)
            .Include(c => c.Transacoes)
            .Select(c => new ListarCarteirasQueryResult(
                c.Id,
                c.Nome,
                c.Transacoes.Sum(t => t.TipoTransacao == TipoTransacao.Entrada ? t.Valor : -t.Valor)
            ))
            .ToListAsync(cancellationToken);
    }
}
