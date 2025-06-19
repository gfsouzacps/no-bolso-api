using System;
using MediatR;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Commands.Transacoes;

public record CriarTransacaoCommand(
    string Descricao,
    decimal Valor,
    NoBolso.Domain.Enums.TipoTransacao TipoTransacao,
    DateTime DataTransacao,
    Guid CarteiraId,
    Guid UsuarioLogadoId
) : IRequest<Guid>;