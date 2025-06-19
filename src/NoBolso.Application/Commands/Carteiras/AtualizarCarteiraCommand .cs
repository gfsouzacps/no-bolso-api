using System;
using MediatR;

namespace NoBolso.Application.Commands.Carteiras
{
    public class RemoverCarteiraCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}