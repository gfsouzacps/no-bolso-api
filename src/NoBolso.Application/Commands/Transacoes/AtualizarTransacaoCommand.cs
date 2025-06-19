using System;
using MediatR;
using NoBolso.Domain.Enums;

namespace NoBolso.Application.Commands.Transacoes;

public record AtualizarTransacaoCommand(
    Guid Id,
    string Descricao,
    decimal Valor,
    TipoTransacao TipoTransacao,
    DateTime DataTransacao,
    Guid GrupoIdDoUsuario // ID do grupo ativo do usuário logado
) : IRequest<Unit>;