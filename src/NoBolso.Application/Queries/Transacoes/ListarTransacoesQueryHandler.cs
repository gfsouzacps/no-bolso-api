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

public class ListarTransacoesQueryHandler : IRequestHandler<ListarTransacoesQuery, List<ListarTransacoesQueryResult>>
{
    private readonly IRepository<Transacao> _transacaoRepository;

    public ListarTransacoesQueryHandler(IRepository<Transacao> transacaoRepository)
    {
        _transacaoRepository = transacaoRepository;
    }

    public async Task<List<ListarTransacoesQueryResult>> Handle(ListarTransacoesQuery request, CancellationToken cancellationToken)
    {
        var query = _transacaoRepository.GetQueryable()
                                        .Include(t => t.Carteira)
                                        .AsQueryable();

        query = AplicarFiltros(query, request);

        return await query
            .OrderByDescending(t => t.DataTransacao)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(t => new ListarTransacoesQueryResult(
                t.Id,
                t.Descricao,
                t.Valor,
                t.TipoTransacao,
                t.DataTransacao,
                t.Carteira.Nome
            ))
            .ToListAsync(cancellationToken);
    }

    private IQueryable<Transacao> AplicarFiltros(IQueryable<Transacao> query, ListarTransacoesQuery request)
    {
        if (request.UsuarioId.HasValue)
            query = query.Where(t => t.Carteira.UsuarioId == request.UsuarioId.Value);

        if (request.CarteiraId.HasValue)
            query = query.Where(t => t.CarteiraId == request.CarteiraId.Value);

        if (request.TipoTransacao.HasValue)
            query = query.Where(t => t.TipoTransacao == request.TipoTransacao.Value);

        if (request.DataInicio.HasValue)
            query = query.Where(t => t.DataTransacao >= request.DataInicio.Value);

        if (request.DataFim.HasValue)
            query = query.Where(t => t.DataTransacao <= request.DataFim.Value);

        return query;
    }
}