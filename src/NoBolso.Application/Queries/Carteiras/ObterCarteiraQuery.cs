using System;
using MediatR;

namespace NoBolso.Application.Queries.Carteiras;

public record ObterCarteiraQuery(Guid Id, bool IncluirTransacoes = false) : IRequest<ObterCarteiraQueryResult?>;

public record ObterCarteiraQueryResult(Guid Id, string Nome, List<TransacaoResumidaResult> Transacoes);
public record TransacaoResumidaResult(Guid Id, string Descricao, decimal Valor);