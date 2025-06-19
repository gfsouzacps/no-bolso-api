using System;
using MediatR;
using NoBolso.Application.DTOs;

namespace NoBolso.Application.Queries.Transacoes
{
    public class ObterTransacaoPorIdQuery : IRequest<TransacaoDto>
    {
        public Guid Id { get; set; }
    }
}