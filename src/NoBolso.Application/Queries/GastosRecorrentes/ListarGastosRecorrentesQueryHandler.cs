using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Application.Queries.GastosRecorrentes;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Enums;
using NoBolso.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.GastosRecorrentes;

public class ListarGastosRecorrentesQueryHandler : IRequestHandler<ListarGastosRecorrentesQuery, List<ListarGastosRecorrentesQueryResult>>
{
    private readonly IRepository<GastoRecorrente> _gastoRepository;

    public ListarGastosRecorrentesQueryHandler(IRepository<GastoRecorrente> gastoRepository)
    {
        _gastoRepository = gastoRepository;
    }

    public async Task<List<ListarGastosRecorrentesQueryResult>> Handle(ListarGastosRecorrentesQuery request, CancellationToken cancellationToken)
    {
        return await _gastoRepository
            .GetQueryable()
            .Where(g => g.GrupoId == request.GrupoId)
            .Include(g => g.CriadoPorUsuario)
            .Select(g => new ListarGastosRecorrentesQueryResult(
                g.Id,
                g.Descricao,
                g.Valor,
                g.CriadoPorUsuario.Nome
            ))
            .ToListAsync(cancellationToken);
    }
}