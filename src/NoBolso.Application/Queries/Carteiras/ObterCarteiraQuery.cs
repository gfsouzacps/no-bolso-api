using System;
using MediatR;
using NoBolso.Application.DTOs;

namespace NoBolso.Application.Queries.Carteiras
{
    public class ObterCarteiraQuery : IRequest<CarteiraDto>
    {
        public Guid Id { get; set; }
        public bool IncluirTransacoes { get; set; } = false;

        public ObterCarteiraQuery(Guid id, bool incluirTransacoes = false)
        {
            Id = id;
            IncluirTransacoes = incluirTransacoes;
        }
    }
}