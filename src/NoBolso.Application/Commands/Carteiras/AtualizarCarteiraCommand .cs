using System;
using MediatR;

namespace NoBolso.Application.Commands.Carteiras;

public record AtualizarCarteiraCommand(Guid Id, string Nome) : IRequest<Unit>;
