using System;
using MediatR;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Commands.Transacoes;

public record CriarTransacaoCommand(
    string Descricao,
    decimal Valor,
    TipoTransacao TipoTransacao,
    DateTime DataTransacao,
    Guid CarteiraId,
    Guid UsuarioId
) : IRequest<Guid>;