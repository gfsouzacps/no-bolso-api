using System;
using MediatR;
using NoBolso.Application.DTOs;

namespace NoBolso.Application.Commands.Carteiras
{
    public class AtualizarCarteiraCommand : IRequest<CarteiraDto>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}