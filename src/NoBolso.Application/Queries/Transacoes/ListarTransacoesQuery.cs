using System;
using System.Collections.Generic;
using MediatR;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Queries.Transacoes;

public record ListarTransacoesQuery(
    Guid GrupoId,
    Guid? CriadoPorUsuarioId,
    Guid? CarteiraId,
    TipoTransacao? TipoTransacao,
    DateTime? DataInicio,
    DateTime? DataFim,
    int PageNumber = 1,
    int PageSize = 25
) : IRequest<List<ListarTransacoesQueryResult>>;


public record ListarTransacoesQueryResult(
    Guid Id,
    string Descricao,
    decimal Valor,
    TipoTransacao TipoTransacao,
    DateTime DataTransacao,
    string NomeCarteira
);