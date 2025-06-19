using MediatR;
using Microsoft.EntityFrameworkCore;
using NoBolso.Domain.Entities;
using NoBolso.Domain.Interfaces;

namespace NoBolso.Application.Queries.Carteiras;

public record ListarCarteirasQuery(Guid UsuarioId) : IRequest<List<ListarCarteirasQueryResult>>;
public record ListarCarteirasQueryResult(Guid Id, string Nome, decimal Saldo);