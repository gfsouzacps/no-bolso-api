using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Application.Queries.Transacoes;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.Transacoes;

public class ObterTransacaoPorIdQueryHandler : IRequestHandler<ObterTransacaoPorIdQuery, ObterTransacaoPorIdQueryResult?>
{
    private readonly IRepository<Transacao> _transacaoRepository;

    public ObterTransacaoPorIdQueryHandler(IRepository<Transacao> transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<ObterTransacaoPorIdQueryResult?> Handle(ObterTransacaoPorIdQuery request, CancellationToken cancellationToken)
    {
        return await _transacaoRepository
            .GetQueryable()
            .Include(t => t.Carteira) // Incluímos a Carteira
            .Where(t => t.Id == request.Id)
            .Select(t => new ObterTransacaoPorIdQueryResult(
                t.Id,
                t.Descricao,
                t.Valor,
                t.TipoTransacao,
                t.DataTransacao,
                t.Carteira.Nome
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}