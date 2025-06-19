using MediatR;
using NoBolso.Domain.Enums;
using System;

namespace NoBolso.Application.Queries.Transacoes;

public record ObterTransacaoPorIdQuery(Guid Id) : IRequest<ObterTransacaoPorIdQueryResult?>;

public record ObterTransacaoPorIdQueryResult(
    Guid Id,
    string Descricao,
    decimal Valor,
    TipoTransacao TipoTransacao,
    DateTime DataTransacao,
    string NomeCarteira
);