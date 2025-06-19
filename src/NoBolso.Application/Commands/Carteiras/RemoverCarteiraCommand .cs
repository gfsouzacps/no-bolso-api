using System;
using MediatR;

namespace NoBolso.Application.Commands.Carteiras;

public record RemoverCarteiraCommand(Guid Id) : IRequest<Unit>;
