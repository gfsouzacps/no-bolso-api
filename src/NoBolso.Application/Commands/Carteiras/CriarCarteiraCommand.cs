using MediatR;

namespace NoBolso.Application.Commands.Carteiras;
public record CriarCarteiraCommand(string Nome, Guid usuarioId) : IRequest<Guid>;