using System;
using MediatR;

namespace NoBolso.Application.Commands.Transacoes;

public record RemoverTransacaoCommand(Guid Id, Guid UsuarioId) : IRequest<Unit>;
