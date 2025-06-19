using System;
using MediatR;

namespace NoBolso.Application.Commands.Transacoes
{
    public class RemoverTransacaoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}