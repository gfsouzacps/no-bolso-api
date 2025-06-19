using MediatR;
using NoBolso.Application.DTOs;

namespace NoBolso.Application.Commands.Carteiras
{
    public class CriarCarteiraCommand : IRequest<CarteiraDto>
    {
        public string Nome { get; set; }
    }
}