using MediatR;
using NoBolso.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBolso.Application.Queries.GastosRecorrentes;

public record ListarGastosRecorrentesQuery(Guid GrupoId) : IRequest<List<ListarGastosRecorrentesQueryResult>>;

public record ListarGastosRecorrentesQueryResult(Guid Id, string Descricao, decimal Valor, string NomeUsuarioCriador);
